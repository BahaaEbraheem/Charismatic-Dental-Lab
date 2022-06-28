using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Roles.Dto;
using Charismatic.Users.Dto;

namespace Charismatic.Users
{
    public interface IUserAppService : ICharismaticAsyncCrudAppService<UserDto, long, CharismaticBaseListInputDto, CreateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
    }
}
