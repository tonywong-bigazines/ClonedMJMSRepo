using Abp.AutoMapper;
using Mahjong.Mahjong;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.PlayHistories.Dto
{
    [AutoMapTo(typeof(PlayHistoryDetailPlayer))]
    public class PlayHistoryDetailPlayerDto
    { 
        public int? Bonus { get; set; }
        public string Position { get; set; }
        public string WinOrLose { get; set; }

        public string PlayerCardId { get; set; }

        public string PlayerType { get; set; }

        public string StaffCardId { get; set; }
    }
}
