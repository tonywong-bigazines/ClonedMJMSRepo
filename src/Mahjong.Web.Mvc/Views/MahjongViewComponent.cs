using Abp.AspNetCore.Mvc.ViewComponents;

namespace Mahjong.Web.Views
{
    public abstract class MahjongViewComponent : AbpViewComponent
    {
        protected MahjongViewComponent()
        {
            LocalizationSourceName = MahjongConsts.LocalizationSourceName;
        }
    }
}
