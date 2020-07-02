using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.Cards.Dto
{
    [AutoMapFrom(typeof(Card))]
    public class CardDto : EntityDto<string>
    {
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string CardType { get; set; }

        public decimal Commission { get; set; }

        public decimal Total { get; set; }
    }
}
