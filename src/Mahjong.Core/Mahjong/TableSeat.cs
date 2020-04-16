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

        public string PlayerCardId { get; set; }
        public Card PlayerCard { get; set; }



    }
}
