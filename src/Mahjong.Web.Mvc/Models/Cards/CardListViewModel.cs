using System.Collections.Generic;
using System.Linq;
using Mahjong.Cards.Dto;
using Mahjong.Mahjong;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mahjong.Web.Models.Cards
{
    public class CardListViewModel
    {
        public IReadOnlyList<CardDto> Cards { get; set; }
        public List<SelectListItem> CardTypeOptions { get; set; }

        public CardListViewModel()
        {
            CardTypeOptions = CardTypes.All.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            }).ToList();
        }
    }
}
