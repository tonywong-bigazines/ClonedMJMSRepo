using Abp.Application.Services.Dto;
using System;

namespace Mahjong.Tables.Dto
{
    //custom PagedResultRequestDto
    public class PagedTableResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
