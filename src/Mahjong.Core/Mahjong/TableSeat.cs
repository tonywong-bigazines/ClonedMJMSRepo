using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Mahjong
{
    public class TableSeat: Entity
    {
        public int TableId { get; set; }
        public Table Table { get; set; }

        public string Position { get; set; }

        public string DeviceConnectionId { get; set; }

        public string PlayerType { get; set; }

        public string PlayerCardId { get; set; }
        public Card PlayerCard { get; set; }
        
        public string StaffCardId { get; set; }
        public Card StaffCard { get; set; }

        public bool HelpPlaying { get; set; }

        public int Round { get; set; }

        public decimal HelpPlayAmount { get; set; }

    }
}
