using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Mahjong
{
    public class PlayHistoryDetailPlayer : Entity
    {
        public int PlayHistoryDetailId { get; set; }
        public PlayHistoryDetail PlayHistoryDetail { get; set; }
        public string PlayerCardId { get; set; }
        public Card PlayerCard { get; set; }
        public string Position { get; set; }
        public string PlayerType { get; set; }
        public string StaffCardId { get; set; }
        public Card StaffCard { get; set; }
        public string WinOrLose { get; set; }
    }
}
