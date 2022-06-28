using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Roles.Dto;

namespace Charismatic.Roles
{
    public interface IRoleAppService : ICharismaticAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
    }
}
