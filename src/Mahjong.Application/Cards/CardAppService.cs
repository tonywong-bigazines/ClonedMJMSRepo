using Abp.Application.Services;
using Abp.Domain.Repositories;
using Mahjong.Mahjong;
using Mahjong.Tables.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Cards
{
    public class CardAppService : AsyncCrudAppService<Table, TableDto, int, PagedTableResultRequestDto, CreateTableDto, TableDto>
    {
        public CardAppService(IRepository<Table> repository)
           : base(repository)
        {

        }
    }
}
