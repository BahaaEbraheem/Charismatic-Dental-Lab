using System.Threading.Tasks;
using Abp.Application.Services;
using Charismatic.Authorization.Accounts.Dto;

namespace Charismatic.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
