using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.ObjectMapping;
using Abp.UI;
using Mahjong.Actions.Dto;
using Mahjong.Cards;
using Mahjong.EntityFrameworkCore;
using Mahjong.Mahjong;
using Mahjong.PlayHistories.Dto;
using Mahjong.Tables.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Tables
{
    public class TableAppService : AsyncCrudAppService<Table, TableDto, int, PagedTableResultRequestDto, CreateTableDto, TableDto>
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<TableSeat> _tableSeatRepository;
        private readonly IRepository<Card,string> _cardRepository;
        private readonly CardAppService _cardAppService;
        private readonly IDbContextProvider<MahjongDbContext> _dbContextProvider;
        private MahjongDbContext _dbContext => _dbContextProvider.GetDbContext();

        public TableAppService(
            IObjectMapper objectMapper,
            IRepository<Table> repository, 
            IRepository<TableSeat> tableSeatRepository,
            CardAppService cardAppService,
            IDbContextProvider<MahjongDbContext> dbContextProvider,
            IRepository<Card, string> cardRepository)
           : base(repository)
        {
            _objectMapper = objectMapper;
            _tableSeatRepository = tableSeatRepository;
            _cardAppService = cardAppService;
            _cardRepository = cardRepository;
            _dbContextProvider = dbContextProvider;
        }

        public async Task<PagedResultDto<TableDto>> GetAllWithSeats(PagedTableResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);

            query = query.Include(x => x.Seats).ThenInclude(x=>x.PlayerCard);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<TableDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }

        public TableDto GetSeats(int tableId)
        {

            var query = Repository.GetAll().Include(x => x.Seats).ThenInclude(x=>x.PlayerCard).FirstOrDefault(x=>x.Id==tableId);


            return MapToEntityDto(query);
        }

        public TableInfoDto GetTableInfo(int tableId)
        {
            var table = Repository.Get(tableId);
            if (table == null)
            {
                throw new UserFriendlyException("Table not exist.");
            }
            var tableInfo = new TableInfoDto();
            tableInfo.Id = table.Id;
            tableInfo.MaxAmount = table.MaxAmount;
            tableInfo.MinAmount = table.MinAmount;
            tableInfo.Name = table.Name;
            tableInfo.Description = table.Description;
            tableInfo.Commission1 = table.MaxAmount * table.PayCommissionRate1;
            tableInfo.Commission2 = table.MaxAmount * table.PayCommissionRate2;
            tableInfo.Commission3 = table.MaxAmount * table.PayCommissionRate3;
            tableInfo.Commission4 = table.MaxAmount * table.PayCommissionRate4;
            tableInfo.Commission5 = table.MaxAmount * table.PayCommissionRate5;
            tableInfo.Commission6 = table.MaxAmount * table.PayCommissionRate6;
            tableInfo.Commission7 = table.MaxAmount * table.PayCommissionRate7;
            tableInfo.Commission8 = table.MaxAmount * table.PayCommissionRate8;
            tableInfo.Commission9 = table.MaxAmount * table.PayCommissionRate9;
            return tableInfo;
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
        public object CheckIn(int tableId, string position, string playerCardId)
        {
            _cardAppService.CardTypeVerification(playerCardId, CardTypes.Client, CardTypes.Staff, CardTypes.FakeClient);

            var card = _cardRepository.Get(playerCardId);

            CheckOutByPlayerCardId(playerCardId);

            var seat = _tableSeatRepository.GetAll().FirstOrDefault(x => x.TableId == tableId && x.Position == position);
            seat.PlayerType = card.CardType == CardTypes.Client ? PlayerTypesEnum.客人 : PlayerTypesEnum.戥脚;
            seat.PlayerCardId = playerCardId;
            CurrentUnitOfWork.SaveChanges();

            return new { Commission = card.Commission, CardType = card.CardType };
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
            seat.Round = 0;
            seat.HelpPlayAmount = 0;

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

        public List<PlayHistoryDto> GetTableHistories(int tableId)
        {
            var playHistories = _dbContext.PlayHistoreis
                .Include(x => x.PlayHistoryDetails)
                .ThenInclude(phd => phd.Players)
                .Where(x => x.TableId == tableId && x.IsPlaying == true)
                .OrderBy(x => x.CreationTime)
                .ToList();
            var result = _objectMapper.Map<List<PlayHistoryDto>>(playHistories);
            return result;
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
