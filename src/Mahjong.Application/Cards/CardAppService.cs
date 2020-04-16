using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Mahjong.Cards.Dto;
using Mahjong.Mahjong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong.Cards
{
    public class CardAppService : AsyncCrudAppService<Card, CardDto, string, PagedCardResultRequestDto, CreateCardDto, CardDto>
    {
        public CardAppService(IRepository<Card,string> repository)
           : base(repository)
        {

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
