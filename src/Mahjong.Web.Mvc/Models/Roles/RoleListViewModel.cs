using System.Collections.Generic;
using Mahjong.Roles.Dto;

namespace Mahjong.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
