using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;
using Mahjong.TableSeats.Dto;

namespace Mahjong.Tables.Dto
{
    [AutoMapFrom(typeof(Table))]
    public class TableDto : EntityDto<int>
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

        public virtual List<TableSeatDto> Seats { get; set; }

        public decimal UnpaidCommissionTotal{
            get {
                decimal total = 0;
                if (Seats != null)
                {
                    foreach (var seat in Seats)
                    {
                        if (!(seat.PlayerType == PlayerTypesEnum.戥脚 && seat.StaffCard == null) && seat.PlayerCard!=null)//非直接戥腳
                        {
                            total += seat.PlayerCard.Commission;
                        }
                    }
                }
                return total;
            }
        }

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
