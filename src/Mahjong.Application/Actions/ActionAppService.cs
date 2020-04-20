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

namespace Mahjong.Actions
{
    public class ActionAppService: AbpServiceBase,IApplicationService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Table> _tableRepository;
        private readonly IRepository<Card,string> _cardRepository;
        private readonly IRepository<PlayHistory> _playHistoryRepository;
        private readonly IRepository<PlayHistoryDetail> _playHistoryDetailRepository;
        private MahjongDbContext _dbContext => _dbContextProvider.GetDbContext();
        private readonly IDbContextProvider<MahjongDbContext> _dbContextProvider;

        public ActionAppService(IObjectMapper objectMapper,
            IRepository<Table> tableRepository, 
            IRepository<Card,string> cardRepository,
            IRepository<PlayHistory> playHistoryRepository,
            IRepository<PlayHistoryDetail> playHistoryDetailRepository,
            IDbContextProvider<MahjongDbContext> dbContextProvider)
        {
            _objectMapper = objectMapper;
            _tableRepository = tableRepository;
            _cardRepository = cardRepository;
            _playHistoryRepository = playHistoryRepository;
            _playHistoryDetailRepository = playHistoryDetailRepository;
            _dbContextProvider = dbContextProvider;
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

            /*foreach (var player in input.Players)
            { 
                //驗證 player card | staff card ?
            }*/

            var table = _tableRepository.Get(input.TableId);

            var playHistory = _playHistoryRepository.GetAll().FirstOrDefault(x => x.TableId == input.TableId && x.Round == table.Round && x.IsPlaying == true);

            var isNewRound = playHistory == null;

            var playerEntities = _objectMapper.Map<List<PlayHistoryDetailPlayer>>(input.Players);

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
            if (ActionsEnum.EndRoundActions.Contains(input.MahjongActionName))
            {
                table.Round += 1;
            }
        }


        public List<PlayHistoryDto> GetPlayHistories(int tableId, string position)
        {
            var playHistories = _dbContext.PlayHistoreis
                .Include(x => x.PlayHistoryDetails)
                .ThenInclude(phd => phd.Players)
                .Where(x => x.TableId == tableId && x.IsPlaying == true && x.PlayHistoryDetails.Any(phd => phd.Players.Any(plr => plr.Position == position)))
                .OrderBy(x => x.CreationTime)
                .ToList();
            var result = _objectMapper.Map<List<PlayHistoryDto>>(playHistories);
            return result;
        }
    }
}
