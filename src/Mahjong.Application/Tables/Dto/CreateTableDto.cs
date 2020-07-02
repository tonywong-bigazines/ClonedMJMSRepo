using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.Tables.Dto
{
    [AutoMapTo(typeof(Table))]
    public class CreateTableDto
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public decimal CommissionRate { get; set; }

        public int Round { get; set; }

        public decimal PayCommissionRate1 { get; set; }
        public decimal PayCommissionRate2 { get; set; }
        public decimal PayCommissionRate3 { get; set; }
        public decimal PayCommissionRate4 { get; set; }
        public decimal PayCommissionRate5 { get; set; }
        public decimal PayCommissionRate6 { get; set; }
        public decimal PayCommissionRate7 { get; set; }
        public decimal PayCommissionRate8 { get; set; }
        public decimal PayCommissionRate9 { get; set; }
    }
}
