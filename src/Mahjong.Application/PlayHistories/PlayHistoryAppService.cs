using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.ObjectMapping;
using Abp.UI;
using Mahjong.Cards;
using Mahjong.EntityFrameworkCore;
using Mahjong.Mahjong;
using Mahjong.PlayHistories.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.PlayHistories
{
    public class PlayHistoryAppService : AsyncCrudAppService<PlayHistory, PlayHistoryDto, int, PagedPlayHistoryResultRequestDto, CreatePlayHistoryDto, PlayHistoryDto>
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IDbContextProvider<MahjongDbContext> _dbContextProvider;
        private MahjongDbContext _dbContext => _dbContextProvider.GetDbContext();

        public PlayHistoryAppService(
            IObjectMapper objectMapper,
            IRepository<PlayHistory> repository, 
            CardAppService cardAppService,
            IDbContextProvider<MahjongDbContext> dbContextProvider,
            IRepository<Card, string> cardRepository)
           : base(repository)
        {
            _objectMapper = objectMapper;
            _dbContextProvider = dbContextProvider;
        }

    
    }
}
