using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Mahjong.Controllers;
using Mahjong.Tables;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Mahjong.Web.Models.Tables;
using Mahjong.Actions;
using Mahjong.TableSeats;
using Mahjong.TableSeats.Dto;
using System.Threading;
using Mahjong.Mahjong;
using Mahjong.PlayHistories;
using Mahjong.Cards;

namespace Mahjong.Web.Controllers
{
    public class TablesController : MahjongControllerBase
    {
        private readonly TableAppService _tableAppService;
        private readonly TableSeatAppService _tableSeatAppService;
        private readonly ActionAppService _actionAppService;
        private readonly PlayHistoryAppService _playHistoryAppService;
        private readonly CardAppService _cardAppService;


        public TablesController(TableAppService tableAppService, 
            ActionAppService actionAppService, 
            TableSeatAppService tableSeatAppService,
            PlayHistoryAppService playHistoryAppService,
            CardAppService cardAppService)
        {
            _tableAppService = tableAppService;
            _actionAppService = actionAppService;
            _tableSeatAppService = tableSeatAppService;
            _playHistoryAppService = playHistoryAppService;
            _cardAppService = cardAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> EditModal(int tableId)
        {
            var table = await _tableAppService.GetAsync(new EntityDto<int>(tableId));
            var model = new EditTableModalViewModel
            {
                Table = table
            };
            return PartialView("_EditModal", model);
        }

        public ActionResult HistoryModal(int tableId)
        {
            var histories = _actionAppService.GetTableHistories(tableId);
           
            return PartialView("_HistoryModal", histories);
        }

        public ActionResult TableSeatsModal(int tableId)
        {
            var table = _tableAppService.GetSeats(tableId);
            var model = new EditTableModalViewModel
            {
                Table = table
            };
            return PartialView("_TableSeatsModal", model);
        }

        public async Task<ActionResult> ClearData()
        {
            var tables = await  _tableAppService.GetAllAsync(new Tables.Dto.PagedTableResultRequestDto() { 
                MaxResultCount = 1000,
                SkipCount= 0
            });

            foreach (var table in tables.Items)
            {
                table.Round = 0;
                await _tableAppService.UpdateAsync(table);
            }

            var tableSeats = await _tableSeatAppService.GetAllAsync(new PagedTableSeatResultRequestDto()
            {
                MaxResultCount = 1000,
                SkipCount = 0
            });

            foreach (var item in tableSeats.Items)
            {
                item.StaffCardId = null;
                item.PlayerCardId = null;
                item.HelpPlayAmount = 0;
                item.DeviceConnectionId = null;
                item.PlayerType = null;
                item.Round = 0;
                await _tableSeatAppService.UpdateAsync(item);
            }

            var histories = await _playHistoryAppService.GetAllAsync(new PlayHistories.Dto.PagedPlayHistoryResultRequestDto() { 
                SkipCount = 0,
                MaxResultCount = 10000
            });

            foreach (var item in histories.Items)
            {                
                await _playHistoryAppService.DeleteAsync(item);
            }

            var cards = await _cardAppService.GetAllAsync(new Cards.Dto.PagedCardResultRequestDto()
            {
                MaxResultCount = 1000,
                SkipCount = 0
            });

            foreach (var item in cards.Items)
            {
                item.Commission = 0;
                item.Total = 0;          
                await _cardAppService.UpdateAsync(item);
            }

            return Content("Done");
        }
    }
}
