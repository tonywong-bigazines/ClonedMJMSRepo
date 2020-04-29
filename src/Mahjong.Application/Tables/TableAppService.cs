using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Mahjong.Cards;
using Mahjong.Mahjong;
using Mahjong.Tables.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Tables
{
    public class TableAppService : AsyncCrudAppService<Table, TableDto, int, PagedTableResultRequestDto, CreateTableDto, TableDto>
    {
        private readonly IRepository<TableSeat> _tableSeatRepository;
        private readonly CardAppService _cardAppService;

        public TableAppService(IRepository<Table> repository, IRepository<TableSeat> tableSeatRepository, CardAppService cardAppService)
           : base(repository)
        {
            _tableSeatRepository = tableSeatRepository;
            _cardAppService = cardAppService;
        }

        public override async Task<TableDto> CreateAsync(CreateTableDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);

            foreach (var position in TablePositionsEnum.All)
            {
                entity.Seats.Add(new TableSeat() { 
                    Position = position
                });
            }
            
            await Repository.InsertAsync(entity);

            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(entity); ;
        }

        /// <summary>
        /// 入座
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="position"></param>
        /// <param name="playerCardId"></param>
        [HttpGet]
        public void CheckIn(int tableId, string position, string playerCardId)
        {
            _cardAppService.CardTypeVerification(playerCardId, CardTypes.Client, CardTypes.Staff, CardTypes.FakeClient);

            CheckOutByPlayerCardId(playerCardId);

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            seat.PlayerType = PlayerTypesEnum.客人;
            seat.PlayerCardId = playerCardId;
            CurrentUnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 離座 / 退出
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="position"></param>
        [HttpGet]
        public void CheckOut(int tableId, string position)
        {
            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            if (seat != null)
            {
                seat.PlayerCardId = null;
                CurrentUnitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// 代打
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="position"></param>
        /// <param name="staffCardId"></param>
        [HttpGet]
        public void HelpClientPlay(int tableId, string position, string staffCardId)
        {
            _cardAppService.CardTypeVerification(staffCardId,CardTypes.Staff);

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);

            if (string.IsNullOrEmpty(seat.PlayerCardId))
            {
                throw new UserFriendlyException("No player in current seat.");
            }

            seat.PlayerType = PlayerTypesEnum.代打;
            seat.StaffCardId = staffCardId;

            CurrentUnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 结束代打
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="position"></param>
        /// <param name="staffCardId"></param>
        [HttpGet]
        public void EndHelpClientPlay(int tableId, string position, string staffCardId)
        {
            _cardAppService.CardTypeVerification(staffCardId, CardTypes.Staff);

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);

            if (string.IsNullOrEmpty(seat.PlayerCardId))
            {
                throw new UserFriendlyException("No player in current seat.");
            }

            seat.PlayerType = PlayerTypesEnum.客人;
            seat.StaffCardId = null;

            CurrentUnitOfWork.SaveChanges();
        }


        /// <summary>
        /// 戥脚
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="position"></param>
        /// <param name="staffCardId"></param>
        [HttpGet]
        public void StaffPlay(int tableId, string position, string staffCardId)
        {
            _cardAppService.CardTypeVerification(staffCardId, CardTypes.Staff);

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);

            if (string.IsNullOrEmpty(seat.PlayerCardId))
            {
                throw new UserFriendlyException("No player in current seat.");
            }

            seat.PlayerType = PlayerTypesEnum.戥脚;
            seat.StaffCardId = staffCardId;

            CurrentUnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 结束戥脚
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="position"></param>
        /// <param name="staffCardId"></param>
        [HttpGet]
        public void EndStaffPlay(int tableId, string position, string staffCardId)
        {
            _cardAppService.CardTypeVerification(staffCardId, CardTypes.Staff);

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);

            if (string.IsNullOrEmpty(seat.PlayerCardId))
            {
                throw new UserFriendlyException("No player in current seat.");
            }

            seat.PlayerType = PlayerTypesEnum.客人;
            seat.StaffCardId = null;

            CurrentUnitOfWork.SaveChanges();
        }


        [UnitOfWork]
        [RemoteService(false)]
        public virtual void RegisterDevice(int tableId, string position,string connectionId)
        {
            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            if (seat != null)
            {
                seat.DeviceConnectionId = connectionId;
                CurrentUnitOfWork.SaveChanges();
            }
        }

        private void CheckOutByPlayerCardId(string playerCardId)
        {
            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.PlayerCardId == playerCardId);
            if (seat != null)
            {
                seat.PlayerCardId = null;
                CurrentUnitOfWork.SaveChanges();
            }
        }
    }
}
