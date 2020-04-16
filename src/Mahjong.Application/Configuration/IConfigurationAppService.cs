using System.Threading.Tasks;
using Mahjong.Configuration.Dto;

namespace Mahjong.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
