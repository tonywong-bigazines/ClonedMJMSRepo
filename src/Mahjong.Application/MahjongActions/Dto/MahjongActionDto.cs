using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using Mahjong.Mahjong;

namespace Mahjong.MahjongActions.Dto
{
    [AutoMapFrom(typeof(MahJongAction))]
    public class MahjongActionDto : EntityDto
    {
        public string Name { get; set; }
        public string CommissionFormula { get; set; }
    }
}
