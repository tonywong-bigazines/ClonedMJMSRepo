using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Mahjong.Cards;
using Mahjong.Mahjong;
using Mahjong.Actions.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.ObjectMapping;
using Mahjong.EntityFrameworkCore;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Mahjong.SignalRService;
using System.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Mahjong.PlayHistories.Dto;

namespace Mahjong.Actions
{
    public class ActionAppService: AbpServiceBase,IApplicationService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<MahJongAction> _mjActionRepository;
        private readonly IRepository<PayCommissionRecord> _payCommissionRecordRepository;
        private readonly IRepository<Table> _tableRepository;
        private readonly IRepository<TableSeat> _tableSeatRepository;
        private readonly IRepository<Card,string> _cardRepository;
        private readonly IRepository<PlayHistory> _playHistoryRepository;
        private readonly IRepository<PlayHistoryDetail> _playHistoryDetailRepository;
        private MahjongDbContext _dbContext => _dbContextProvider.GetDbContext();
        private readonly IDbContextProvider<MahjongDbContext> _dbContextProvider;
        private readonly IHubContext<RecordHub> _hubContext;


        public ActionAppService(
            IObjectMapper objectMapper,
            IRepository<Table> tableRepository,
            IRepository<PayCommissionRecord> payCommissionRecordRepository,
            IRepository<TableSeat> tableSeatRepository,
            IRepository<Card,string> cardRepository,
            IRepository<PlayHistory> playHistoryRepository,
            IRepository<PlayHistoryDetail> playHistoryDetailRepository,
            IDbContextProvider<MahjongDbContext> dbContextProvider,
            IRepository<MahJongAction> mjActionRepository,
            IHubContext<RecordHub> hubContext)
        {
            _objectMapper = objectMapper;
            _tableRepository = tableRepository;
            _tableSeatRepository = tableSeatRepository;
            _cardRepository = cardRepository;
            _playHistoryRepository = playHistoryRepository;
            _playHistoryDetailRepository = playHistoryDetailRepository;
            _dbContextProvider = dbContextProvider;
            _hubContext = hubContext;
            _mjActionRepository = mjActionRepository;
            _payCommissionRecordRepository = payCommissionRecordRepository;
        }

        

        public void Create(CreateActionDto input)
        {
            var isValidAction = ActionsEnum.All.Contains(input.MahjongActionName);
            if (!isValidAction)
            {
                throw new UserFriendlyException("Invalid action.");
            }

            var tableExist = _tableRepository.GetAll().Any(x => x.Id == input.TableId);
            if (!tableExist)
            {
                throw new UserFriendlyException("Invalid table id.");
            }

            var isValidOperator = _cardRepository.GetAll().Any(x => x.Id == input.OperatorCardId && x.CardType == CardTypes.Staff);
            if (!isValidOperator)
            {
                throw new UserFriendlyException("Invalid operator id.");
            }

            var table = _tableRepository.GetAllIncluding(x=>x.Seats).FirstOrDefault(x=>x.Id == input.TableId);

            var playHistory = _playHistoryRepository.GetAll().FirstOrDefault(x => x.TableId == input.TableId && x.Round == table.Round && x.IsPlaying == true);

            var isNewRound = playHistory == null;

            var playerEntities = new List<PlayHistoryDetailPlayer>();

            var relatedSeats = table.Seats.Where(x => input.Players.Any(m => m.Position == x.Position)).ToList();

            foreach (var seat in relatedSeats)
            {
                var player = new PlayHistoryDetailPlayer() {
                    PlayerCardId = seat.PlayerCardId,
                    PlayerType = seat.PlayerType,
                    Position = seat.Position,
                    StaffCardId = seat.StaffCardId,
                    WinOrLose = input.Players.FirstOrDefault(x => x.Position == seat.Position).WinOrLose,
                    Bonus = input.Players.FirstOrDefault(x => x.Position == seat.Position).Bonus ?? 1
                };
                playerEntities.Add(player);
            }


            if (isNewRound)
            {
                playHistory = new PlayHistory()
                {
                    IsPlaying = true,
                    TableId = table.Id,
                    Round = table.Round
                };

                _playHistoryRepository.Insert(playHistory);
                CurrentUnitOfWork.SaveChanges();
            }

            var newPlayHistoryDetail = new PlayHistoryDetail()
            {
                OperatorCardId = input.OperatorCardId,
                MohjongActionName = input.MahjongActionName,
                Players = playerEntities,
                PlayHistoryId = playHistory.Id
            };

            _playHistoryDetailRepository.Insert(newPlayHistoryDetail);

            var actionDto = _objectMapper.Map<PlayHistoryDetailDto>(newPlayHistoryDetail);
            var playHistoryDto = new PlayHistoryDto() { 
                TableId = table.Id,
                Round = table.Round,
                PlayHistoryDetails = new List<PlayHistoryDetailDto>() {
                    actionDto
                }
            };

            //結束當前回合的標誌Action
            if (ActionsEnum.RoundEndActions.Contains(input.MahjongActionName))
            {
                table.Round += 1;
            }

            PushNewRecord(playHistoryDto);
  
            foreach (var seat in relatedSeats)
            {
                var player = playerEntities.FirstOrDefault(x=>x.Position == seat.Position);
               
                
                var winners = playerEntities.Where(x => x.WinOrLose == "Win").Count();
                var amount = EvaluateWinOrLoseAmount(input.MahjongActionName, table, player.WinOrLose, winners);

                if (player.PlayerType == PlayerTypesEnum.戥脚)
                {
                    var staffCardId = string.IsNullOrEmpty(seat.StaffCardId) ? seat.PlayerCardId : seat.StaffCardId;
                    var staffCard = _cardRepository.Get(staffCardId);
                    staffCard.Total += amount;
                }
                else
                {
                    var playerCard = _cardRepository.Get(player.PlayerCardId);
                    playerCard.Total += amount;

                    if (player.IsWinner)
                    {
                        //計算Commission
                        var commission = EvaluateCommission(input.MahjongActionName, table, player.Bonus);

                        playerCard.Commission += commission;

                        PushCommission(table.Id, player.Position, playerCard.Commission);
                    }
                }

                //計算代打輸贏
                if (seat.PlayerType == PlayerTypesEnum.代打)
                {
                    if (ActionsEnum.RoundEndActions.Contains(input.MahjongActionName))
                    {
                        seat.Round += 1;
                    }

                    seat.HelpPlayAmount += amount;

                    PushHelpPlayInfo(table.Id, seat.Position, seat.Round, seat.HelpPlayAmount);
                }
            }

            CurrentUnitOfWork.SaveChanges();
        }
        [HttpGet]
        public void PayAllCommission(int tableId, string position, string operatorId)
        {
            var tableExist = _tableRepository.GetAll().Any(x => x.Id == tableId);
            if (!tableExist)
            {
                throw new UserFriendlyException("Invalid table id.");
            }

            var isValidOperator = _cardRepository.GetAll().Any(x => x.Id == operatorId && x.CardType == CardTypes.Staff);
            if (!isValidOperator)
            {
                throw new UserFriendlyException("Invalid operator id.");
            }

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);

            if (seat == null)
            {
                throw new UserFriendlyException("Invalid parameters.");
            }

            if (seat.PlayerType == PlayerTypesEnum.戥脚)
            {
                throw new UserFriendlyException("[戥脚] can not do this.");
            }

            var playerCard = _cardRepository.Get(seat.PlayerCardId);
            var commission = playerCard.Commission;
            playerCard.Commission  = 0;

            var record = new PayCommissionRecord();
            record.Amount = commission;
            record.PlayerCardId = playerCard.Id;
            record.OperatorCardId = operatorId;

            _payCommissionRecordRepository.Insert(record);

            PushCommission(tableId, position, 0);
            CurrentUnitOfWork.SaveChanges();
        }
        [HttpGet]
        public void PayCommission(int tableId, string position, decimal amount, string operatorId)
        {
            var tableExist = _tableRepository.GetAll().Any(x => x.Id == tableId);
            if (!tableExist)
            {
                throw new UserFriendlyException("Invalid table id.");
            }
            
            var isValidOperator = _cardRepository.GetAll().Any(x => x.Id == operatorId && x.CardType == CardTypes.Staff);
            if (!isValidOperator)
            {
                throw new UserFriendlyException("Invalid operator id.");
            }

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);

            if (seat == null)
            {
                throw new UserFriendlyException("Invalid parameters.");
            }

            if (seat.PlayerType == PlayerTypesEnum.戥脚)
            {
                throw new UserFriendlyException("[戥脚] can not do this.");
            }

            var playerCard = _cardRepository.Get(seat.PlayerCardId);

            if (playerCard.Commission - amount < 0) 
            {
                throw new UserFriendlyException($"Commissioin is letter than {amount}");
            }

            playerCard.Commission -= amount;

            var record = new PayCommissionRecord();
            record.Amount = amount;
            record.PlayerCardId = playerCard.Id;
            record.OperatorCardId = operatorId;

            _payCommissionRecordRepository.Insert(record);

            PushCommission(tableId, position, playerCard.Commission);
            CurrentUnitOfWork.SaveChanges();
        }


        private decimal EvaluateCommission(string mahjongActionName, Table table, int bonus)
        {
            var mjAction = _mjActionRepository.GetAll().FirstOrDefault(x => x.Name == mahjongActionName);

            if (mjAction == null)
            {
                throw new UserFriendlyException("Mahjong action not exist.");
            }

            var expression = mjAction.CommissionFormula
                .Replace("MinAmount", table.MinAmount.ToString())
                .Replace("MaxAmount", table.MaxAmount.ToString())
                .Replace("CommissionRate", table.CommissionRate.ToString())
                .Replace("Bonus", bonus.ToString());

            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(decimal), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (decimal)(loDataTable.Rows[0]["Eval"]);
        }
        private decimal EvaluateWinOrLoseAmount(string mahjongActionName, Table table, string winOrLose, int winners=1)
        {
            var mjAction = _mjActionRepository.GetAll().FirstOrDefault(x => x.Name == mahjongActionName);

            if (mjAction == null)
            {
                throw new UserFriendlyException("Mahjong action not exist.");
            }

            string expression;
            if (winOrLose == "Lose")
            {
                expression = mjAction.LoseAmountFormula
                    .Replace("MinAmount", table.MinAmount.ToString())
                    .Replace("MaxAmount", table.MaxAmount.ToString())
                    .Replace("Winners", winners.ToString());
            }
            else 
            {
                expression = mjAction.WinAmountFormula
                    .Replace("MinAmount", table.MinAmount.ToString())
                    .Replace("MaxAmount", table.MaxAmount.ToString());
            }

            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(decimal), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (decimal)(loDataTable.Rows[0]["Eval"]) * (winOrLose == "Lose"?-1:1);
        }

        private async void PushNewRecord(PlayHistoryDto playHistoryDto)
        {
            var seats = _tableSeatRepository.GetAll().Where(x => x.TableId == playHistoryDto.TableId).ToList();
            if (seats != null)
            {
                var connections = seats.Select(x => x.DeviceConnectionId).ToList();
                await _hubContext.Clients.Clients(connections).SendAsync("newRecord", playHistoryDto);
            }
        }

        private async void PushHelpPlayInfo(int tableId, string position, int round, decimal amount)
        {
            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            if (seat != null)
            {
                await _hubContext.Clients.Client(seat.DeviceConnectionId).SendAsync("helpPlayInfo", new { tableId,position,round,amount });
            }
        }

        private async void PushCommission(int tableId, string position, decimal commission)
        {
            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            if (seat != null)
            {
                await _hubContext.Clients.Client(seat.DeviceConnectionId).SendAsync("commissionUpdate", commission);
            }            
        }

        public List<PlayHistoryDto> GetTableHistories(int tableId)
        {
            var playHistories = _dbContext.PlayHistoreis
                .Include(x => x.PlayHistoryDetails)
                .ThenInclude(phd => phd.Players)
                .Where(x => x.TableId == tableId && x.IsPlaying == true )
                .OrderBy(x => x.CreationTime)
                .ToList();
            var result = _objectMapper.Map<List<PlayHistoryDto>>(playHistories);
            return result;
        }
    }
}
