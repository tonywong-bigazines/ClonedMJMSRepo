using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mahjong.Mahjong
{
    public class Table:FullAuditedEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        
        [MaxLength(50)]
        public string Status { get; set; }

        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }

        public int Round { get; set; }

        public virtual List<TableSeat> Seats { get; set; }

        public Table()
        {
            Seats = new List<TableSeat>();
        }
    }
}
