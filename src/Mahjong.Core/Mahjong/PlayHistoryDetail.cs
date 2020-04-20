using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mahjong.Mahjong
{
    public class PlayHistoryDetail:Entity, IHasCreationTime
    {
        public int PlayHistoryId { get; set; }
        public PlayHistory PlayHistory { get; set; }
        public virtual List<PlayHistoryDetailPlayer> Players { get; set; }

        public string MohjongActionName { get; set; }

        public string OperatorCardId { get; set; }
        public Card OperatorCard { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public PlayHistoryDetail()
        {
            CreationTime = DateTime.Now;
            Players = new List<PlayHistoryDetailPlayer>();
        }
    }
}
