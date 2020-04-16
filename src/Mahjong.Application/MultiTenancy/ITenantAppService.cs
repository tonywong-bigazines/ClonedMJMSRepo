using Abp.Application.Services;
using Mahjong.MultiTenancy.Dto;

namespace Mahjong.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

