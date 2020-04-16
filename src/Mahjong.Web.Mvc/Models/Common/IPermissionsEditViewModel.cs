using System.Collections.Generic;
using Mahjong.Roles.Dto;

namespace Mahjong.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}