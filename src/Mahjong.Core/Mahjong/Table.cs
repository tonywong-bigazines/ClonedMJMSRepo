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

        public DateTime? StartTime { get; set; }

        public int? Amount { get; set; }

        public virtual List<Card> Cards { get; set; }

        public Guid? PlayingKey { get; set; }
    }
}
