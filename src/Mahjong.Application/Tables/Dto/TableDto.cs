using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.Tables.Dto
{
    [AutoMapFrom(typeof(Table))]
    public class TableDto : EntityDto<int>
    {
       
    }
}
