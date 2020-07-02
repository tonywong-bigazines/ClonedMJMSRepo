using Abp.Application.Services.Dto;
using System;

namespace Mahjong.PlayHistories.Dto
{
    //custom PagedResultRequestDto
    public class PagedPlayHistoryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
