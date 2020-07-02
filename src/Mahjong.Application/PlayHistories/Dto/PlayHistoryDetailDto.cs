using Abp.AutoMapper;
using Mahjong.Mahjong;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.PlayHistories.Dto
{
    [AutoMapTo(typeof(PlayHistoryDetail))]
    public class PlayHistoryDetailDto
    {
       
        public virtual List<PlayHistoryDetailPlayerDto> Players { get; set; }

        public string MohjongActionName { get; set; }

        public string OperatorCardId { get; set; }

        public DateTime CreationTime { get; set; }

    }
}
