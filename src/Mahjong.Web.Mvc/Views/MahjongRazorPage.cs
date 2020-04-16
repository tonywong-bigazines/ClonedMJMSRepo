using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Mahjong.Web.Views
{
    public abstract class MahjongRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected MahjongRazorPage()
        {
            LocalizationSourceName = MahjongConsts.LocalizationSourceName;
        }
    }
}
