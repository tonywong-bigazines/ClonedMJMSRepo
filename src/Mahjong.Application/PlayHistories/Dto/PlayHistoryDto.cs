using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Mahjong.Mahjong;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.PlayHistories.Dto
{
    [AutoMapTo(typeof(PlayHistory))]
    public class PlayHistoryDto:EntityDto
    {
        public int TableId { get; set; }

        public int Round { get; set; }

        public virtual List<PlayHistoryDetailDto> PlayHistoryDetails { get; set; }

    }
}
