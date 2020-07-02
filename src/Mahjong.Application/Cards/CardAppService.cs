using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.ObjectMapping;
using Abp.UI;
using Mahjong.Actions.Dto;
using Mahjong.Cards.Dto;
using Mahjong.EntityFrameworkCore;
using Mahjong.Mahjong;
using Mahjong.PlayHistories.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Cards
{
    public class CardAppService : AsyncCrudAppService<Card, CardDto, string, PagedCardResultRequestDto, CreateCardDto, CardDto>
    {
        private readonly IObjectMapper _objectMapper;
        private MahjongDbContext _dbContext => _dbContextProvider.GetDbContext();
        private readonly IDbContextProvider<MahjongDbContext> _dbContextProvider;
        public CardAppService(
            IObjectMapper objectMapper,
            IRepository<Card,string> repository, 
            IDbContextProvider<MahjongDbContext> dbContextProvider)
           : base(repository)
        {
            _objectMapper = objectMapper;
            _dbContextProvider = dbContextProvider;
        }

        public void Liquidation(string cardId)
        {
            var card = GetAsync(new Abp.Application.Services.Dto.EntityDto<string> { Id = cardId }).Result;

            card.Commission = 0;
            card.Total = 0;

            var result = UpdateAsync(card).Result;
        }

        public LiquidationResultDto GetPlayerHistory(string cardId)
        {
            var card = Repository.Get(cardId);

            var data = _dbContext.PlayHistoryDetailPlayers
                .Include(x => x.PlayHistoryDetail)
                .ThenInclude(x => x.PlayHistory)
                .Where(x => x.PlayerCardId == cardId && x.PlayerType != PlayerTypesEnum.戥脚 && x.PlayHistoryDetail.PlayHistory.IsPlaying == true)
                .OrderBy(x => x.Id)
                .ToList();

            var histories = new List<PlayHistoryDto>();

            foreach (var phdp in data)
            {
                var history = histories.FirstOrDefault(x => x.TableId == phdp.PlayHistoryDetail.PlayHistory.TableId && x.Round == phdp.PlayHistoryDetail.PlayHistory.Round);
                if (history == null)
                {
                    history = new PlayHistoryDto()
                    {
                        TableId = phdp.PlayHistoryDetail.PlayHistory.TableId,
                        Round = phdp.PlayHistoryDetail.PlayHistory.Round,
                        PlayHistoryDetails = new List<PlayHistoryDetailDto>()
                    };
                    histories.Add(history);
                }
                history.PlayHistoryDetails.Add(new PlayHistoryDetailDto()
                {
                    MohjongActionName = phdp.PlayHistoryDetail.MohjongActionName,
                    Players = _objectMapper.Map<List<PlayHistoryDetailPlayerDto>>(phdp.PlayHistoryDetail.Players),
                    OperatorCardId = phdp.PlayHistoryDetail.OperatorCardId,
                    CreationTime = phdp.PlayHistoryDetail.CreationTime.AddHours(8)
                });
            }


            return new LiquidationResultDto() {
                Win = data.Count(x => x.IsWinner),
                Lose = data.Count(x=>x.IsLoser),
                Total = card.Total,
                CheckinTime = histories.First().PlayHistoryDetails.First().CreationTime,
                CheckoutTime = histories.Last().PlayHistoryDetails.Last().CreationTime,
                Histories = histories
            };
        }


        [RemoteService(false)]
        public void CardTypeVerification(string cardId, params string[] cardTypes)
        {
            if (CardTypes.All.Any(cardType=> cardTypes.Contains(cardType)) == false)
            {
                throw new UserFriendlyException(404, $"This card type does not exist.");
            }
            var isOneOfTypes = Repository.GetAll().Any(x => x.Id == cardId && cardTypes.Contains(x.CardType));
            if (!isOneOfTypes)
            {
                throw new UserFriendlyException("Invalid card type.");
            }
        }
    }
}
