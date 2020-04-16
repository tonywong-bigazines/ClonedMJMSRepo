using Abp.Application.Services.Dto;
using System;

namespace Mahjong.Cards.Dto
{
    //custom PagedResultRequestDto
    public class PagedCardResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
