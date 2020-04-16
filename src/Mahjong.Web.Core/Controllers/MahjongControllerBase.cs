using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Mahjong.Controllers
{
    public abstract class MahjongControllerBase: AbpController
    {
        protected MahjongControllerBase()
        {
            LocalizationSourceName = MahjongConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
