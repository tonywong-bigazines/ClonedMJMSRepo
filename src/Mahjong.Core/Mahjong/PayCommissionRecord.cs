using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mahjong.Mahjong
{
    public class PayCommissionRecord: FullAuditedEntity
    {
        public string PlayerCardId { get; set; }
        public Card PlayerCard { get; set; }
        public string OperatorCardId { get; set; }
        public Card OperatorCard { get; set; }
        public decimal Amount { get; set; }
    }
}
