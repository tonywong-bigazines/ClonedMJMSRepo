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
    public class CardDto : EntityDto<int>
    {
       
    }
}
