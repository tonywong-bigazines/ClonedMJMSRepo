using Abp.Authorization;
using Mahjong.Authorization.Roles;
using Mahjong.Authorization.Users;

namespace Mahjong.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
