using System.Collections.Generic;
using Mahjong.Roles.Dto;

namespace Mahjong.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
