using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Mahjong.Authorization;

namespace Mahjong.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class MahjongNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tables,
                        L("Tables"),
                        url: "Tables",
                        icon: "fas fa-info-circle"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Cards,
                        L("Cards"),
                        url: "Cards",
                        icon: "fas fa-info-circle"
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MahjongConsts.LocalizationSourceName);
        }
    }
}
