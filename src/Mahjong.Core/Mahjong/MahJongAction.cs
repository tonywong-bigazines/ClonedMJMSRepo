using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mahjong.Mahjong
{
    public class MahJongAction : FullAuditedEntity
    {
        public string Name { get; set; }
        public string CommissionFormula { get; set; }
    }
}
