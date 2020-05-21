using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Mahjong.Cards;
using Mahjong.Mahjong;
using Mahjong.MahjongActions.Dto;
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
using Microsoft.AspNetCore.SignalR;
using Mahjong.SignalRService;
using System.Data;

namespace Mahjong.MahjongActions
{
    public class MahjongActionAppService : AsyncCrudAppService<MahJongAction, MahjongActionDto, int, PagedMahjongActionResultRequestDto, CreateMahjongActionDto, MahjongActionDto>
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Table> _tableRepository;
        private readonly IRepository<TableSeat> _tableSeatRepository;
        private readonly IRepository<Card,string> _cardRepository;
        private MahjongDbContext _dbContext => _dbContextProvider.GetDbContext();
        private readonly IDbContextProvider<MahjongDbContext> _dbContextProvider;
        private readonly IHubContext<RecordHub> _hubContext;


        public MahjongActionAppService(
            IRepository<MahJongAction> repository,
            IObjectMapper objectMapper,
            IRepository<Table> tableRepository,
            IRepository<TableSeat> tableSeatRepository,
            IRepository<Card, string> cardRepository,
            IDbContextProvider<MahjongDbContext> dbContextProvider,
            IHubContext<RecordHub> hubContext) : base(repository)
        {
            _objectMapper = objectMapper;
            _tableRepository = tableRepository;
            _tableSeatRepository = tableSeatRepository;
            _cardRepository = cardRepository;
            _dbContextProvider = dbContextProvider;
            _hubContext = hubContext;
        }


    }
}
