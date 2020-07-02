using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;
using Mahjong.PlayHistories.Dto;

namespace Mahjong.Actions.Dto
{

    public class CreateActionDto
    {
        public int TableId { get; set; }
        public string MahjongActionName { get; set; }

        public List<PlayHistoryDetailPlayerDto> Players { get; set; }

        public string OperatorCardId { get; set; }
    }

    
}
