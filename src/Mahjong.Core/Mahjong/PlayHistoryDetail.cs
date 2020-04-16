using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Mahjong
{
    public class PlayHistoryDetail:Entity, IHasCreationTime
    {
        public virtual List<PlayHistoryDetailPlayer> Players { get; set; }

        public string MohjongActionName { get; set; }

        public string StaffCardId { get; set; }
        public Card StaffCard { get; set; }


        public virtual DateTime CreationTime { get; set; }

        public PlayHistoryDetail()
        {
            CreationTime = DateTime.Now;
            Players = new List<PlayHistoryDetailPlayer>();
        }
    }
}
