using System.Threading.Tasks;
using Abp.Application.Services;
using Charismatic.Sessions.Dto;

namespace Charismatic.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
