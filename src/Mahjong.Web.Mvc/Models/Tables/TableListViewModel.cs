using System.Collections.Generic;
using Mahjong.Tables.Dto;

namespace Mahjong.Web.Models.Tables
{
    public class TableListViewModel
    {
        public IReadOnlyList<TableDto> Tables { get; set; }
    }
}
