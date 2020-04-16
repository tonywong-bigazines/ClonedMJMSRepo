using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mahjong.Mahjong
{
    public class Card: FullAuditedEntity<string>
    {
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string CardType { get; set; }


        public decimal Commission { get; set; }
    }
}
