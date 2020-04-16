using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Mahjong.Mahjong;
using Mahjong.TableSeats.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong.TableSeats
{
    public class TableSeatAppService : AsyncCrudAppService<TableSeat, TableSeatDto, int, PagedTableSeatResultRequestDto, CreateTableSeatDto, TableSeatDto>
    {
        public TableSeatAppService(IRepository<TableSeat> repository)
           : base(repository)
        {

        }

        
    }
}
