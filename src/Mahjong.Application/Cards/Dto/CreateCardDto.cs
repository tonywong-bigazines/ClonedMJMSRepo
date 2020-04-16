using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.Cards.Dto
{
    [AutoMapTo(typeof(Card))]
    public class CreateCardDto:EntityDto<string>
    {
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string CardType { get; set; }
    }
}
