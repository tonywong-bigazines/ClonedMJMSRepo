using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Mahjong
{
    public class PlayHistoryDetail:Entity, IHasCreationTime
    {
        public string Winner1CardId { get; set; }
        public Card Winner1Card { get; set; }
        public string Winner1PlayerType { get; set; }

        public string Winner2CardId { get; set; }
        public Card Winner2Card { get; set; }
        public string Winner2PlayerType { get; set; }

        public string Winner3CardId { get; set; }
        public Card Winner3Card { get; set; }
        public string Winner3PlayerType { get; set; }


        public string Loser1CardId { get; set; }
        public Card Loser1Card { get; set; }
        public string Loser1PlayerType { get; set; }

        public string Loser2CardId { get; set; }
        public Card Loser2Card { get; set; }
        public string Loser2PlayerType { get; set; }

        public string Loser3CardId { get; set; }
        public Card Loser3Card { get; set; }
        public string Loser3PlayerType { get; set; }

        public string MohjongActionName { get; set; }

        public int Amount { get; set; }
        public string StaffCardId { get; set; }
        public Card StaffCard { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public PlayHistoryDetail()
        {
            CreationTime = DateTime.Now;
        }
    }
}
