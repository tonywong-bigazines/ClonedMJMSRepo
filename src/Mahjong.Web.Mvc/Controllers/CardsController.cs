using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Mahjong.Controllers;
using Mahjong.Cards;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Mahjong.Web.Models.Cards;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mahjong.Mahjong;
using System.Linq;

namespace Mahjong.Web.Controllers
{
    public class CardsController : MahjongControllerBase
    {
        private readonly CardAppService _cardAppService;
        

        public CardsController(CardAppService cardAppService)
        {
            _cardAppService = cardAppService;           
        }

        public IActionResult Index()
        {
            
            return View(new CardListViewModel());
        }

        public async Task<ActionResult> EditModal(string cardId)
        {
            var card = await _cardAppService.GetAsync(new EntityDto<string>(cardId));
            var model = new EditCardModalViewModel
            {
                Card = card
            };
            return PartialView("_EditModal", model);
        }
    }
}
