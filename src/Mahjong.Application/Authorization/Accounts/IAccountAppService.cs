using System.Threading.Tasks;
using Abp.Application.Services;
using Mahjong.Authorization.Accounts.Dto;

namespace Mahjong.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
