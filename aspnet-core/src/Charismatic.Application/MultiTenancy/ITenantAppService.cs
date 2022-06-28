using Abp.Application.Services;
using Charismatic.MultiTenancy.Dto;

namespace Charismatic.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

