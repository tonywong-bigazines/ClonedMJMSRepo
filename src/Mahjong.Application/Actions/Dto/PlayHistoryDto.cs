using Abp.AutoMapper;
using Mahjong.Mahjong;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Actions.Dto
{
    [AutoMapTo(typeof(PlayHistory))]
    public class PlayHistoryDto
    {
        public int TableId { get; set; }

        public int Round { get; set; }

        public virtual List<PlayHistoryDetailDto> PlayHistoryDetails { get; set; }

    }
}
