using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.PlayHistories.Dto
{
    [AutoMapTo(typeof(PlayHistory))]
    public class CreatePlayHistoryDto
    {
        
    }
}
