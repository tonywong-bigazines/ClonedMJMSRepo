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

namespace Mahjong.Actions
{
    public class ActionAppService: AbpServiceBase,IApplicationService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Table> _tableRepository;
        private readonly IRepository<TableSeat> _tableSeatRepository;
        private readonly IRepository<Card,string> _cardRepository;
        private readonly IRepository<PlayHistory> _playHistoryRepository;
        private readonly IRepository<PlayHistoryDetail> _playHistoryDetailRepository;
        private MahjongDbContext _dbContext => _dbContextProvider.GetDbContext();
        private readonly IDbContextProvider<MahjongDbContext> _dbContextProvider;
        private readonly IHubContext<RecordHub> _hubContext;


        public ActionAppService(IObjectMapper objectMapper,
            IRepository<Table> tableRepository,
            IRepository<TableSeat> tableSeatRepository,
            IRepository<Card,string> cardRepository,
            IRepository<PlayHistory> playHistoryRepository,
            IRepository<PlayHistoryDetail> playHistoryDetailRepository,
            IDbContextProvider<MahjongDbContext> dbContextProvider,
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
                    WinOrLose = input.Players.FirstOrDefault(x => x.Position == seat.Position).WinOrLose
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

            //結束當前回合的標誌Action
            if (ActionsEnum.RoundEndActions.Contains(input.MahjongActionName))
            {
                table.Round += 1;
            }
            
            var actionDto = _objectMapper.Map<PlayHistoryDetailDto>(newPlayHistoryDetail);
            PushNewRecord(table.Id, actionDto);
        }

        private async void PushNewRecord(int tableId, PlayHistoryDetailDto action)
        {
            var seats = _tableSeatRepository.GetAll().Where(x => x.TableId == tableId).ToList();
            if (seats != null)
            {
                var connections = seats.Select(x => x.DeviceConnectionId).ToList();
                await _hubContext.Clients.Clients(connections).SendAsync("newRecord", action);
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
