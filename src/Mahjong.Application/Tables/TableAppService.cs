using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Mahjong.Mahjong;
using Mahjong.Tables.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Tables
{
    [AbpAllowAnonymous]
    public class TableAppService : AsyncCrudAppService<Table, TableDto, int, PagedTableResultRequestDto, CreateTableDto, TableDto>
    {
        public TableAppService(IRepository<Table> repository)
           : base(repository)
        {

        }
    }
}
