using System.Threading.Tasks;
using Abp.Application.Services;
using Mahjong.Sessions.Dto;

namespace Mahjong.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
