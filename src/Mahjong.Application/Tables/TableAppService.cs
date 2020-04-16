using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
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

        [HttpGet]
        public void TakeSeat(int tableId, string position, string playerCardId)
        {
            _cardAppService.CardTypeVerification(playerCardId, CardTypes.Client, CardTypes.Staff, CardTypes.FakeClient);

            OffSeatByPlayerCardId(playerCardId);

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            var hasBeenTaked = seat != null && seat.PlayerCardId != null;
            if (hasBeenTaked)
            {
                throw new UserFriendlyException("This seat is already occupied.");
            }
            seat.PlayerCardId = playerCardId;
            CurrentUnitOfWork.SaveChanges();
        }

        [HttpGet]
        public void OffSeat(int tableId, string position)
        {
            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            if (seat != null)
            {
                seat.PlayerCardId = null;
                CurrentUnitOfWork.SaveChanges();
            }
        }

        private void OffSeatByPlayerCardId(string playerCardId)
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
