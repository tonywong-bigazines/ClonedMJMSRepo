using System.Collections.Generic;
using System.Linq;
using Mahjong.Roles.Dto;
using Mahjong.Cards.Dto;
using Mahjong.Users.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mahjong.Mahjong;

namespace Mahjong.Web.Models.Cards
{
    public class EditCardModalViewModel
    {
        public CardDto Card { get; set; }
        public List<SelectListItem> CardTypeOptions { get; set; }

        public EditCardModalViewModel() {
            CardTypeOptions = CardTypes.All.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            }).ToList();
        }
    }
}
