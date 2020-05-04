using Abp.AutoMapper;
using Mahjong.Mahjong;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Actions.Dto
{
    [AutoMapTo(typeof(PlayHistoryDetailPlayer))]
    public class PlayHistoryDetailPlayerDto
    { 
        public string Position { get; set; }
        public string WinOrLose { get; set; }
    }
}
