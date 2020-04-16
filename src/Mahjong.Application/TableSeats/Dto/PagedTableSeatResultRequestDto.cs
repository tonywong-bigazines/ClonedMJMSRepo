using Abp.Application.Services.Dto;
using System;

namespace Mahjong.TableSeats.Dto
{
    //custom PagedResultRequestDto
    public class PagedTableSeatResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
