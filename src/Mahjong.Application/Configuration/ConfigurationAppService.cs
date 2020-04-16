using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Mahjong.Configuration.Dto;

namespace Mahjong.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MahjongAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
