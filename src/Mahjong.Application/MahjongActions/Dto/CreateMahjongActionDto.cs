using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.MahjongActions.Dto
{

    public class CreateMahjongActionDto
    {
        public string Name { get; set; }
        public string CommissionFormula { get; set; }
    }

    
}
