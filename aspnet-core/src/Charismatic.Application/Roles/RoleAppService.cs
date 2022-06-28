using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.UI;
using Charismatic.Authorization;
using Charismatic.Authorization.Roles;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.Localization.SourceFiles;
using Charismatic.Roles.Dto;
using ITLand.CMMS.Libs.DevExtreme;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Charismatic.Roles
{
  //  [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : CharismaticAsyncCrudAppService<Role, RoleDto, int, CharismaticBaseListInputDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public override async Task<RoleDto> CreateAsync(CreateRoleDto input)
        {
            if (input.GrantedPermissions == null || input.GrantedPermissions.Count == 0)
                throw new UserFriendlyException(Exceptions.AtLeastOneOfThesePermissionsMustBeGranted);

            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.IsActive = true;
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        public override async Task<RoleDto> UpdateAsync(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }
        public async Task<int> GetUsersCountForRole(GetRolesInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            return users.Count;
        }

        public async Task<ListResultDto<PermissionDto>> GetGrantedPermissions(int id)
        {
            var permissions = await _roleManager.GetGrantedPermissionsAsync(id);

            return new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList()
            );
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList()
            ));
        }
        [RemoteService(IsEnabled = false)]
        public override Task<PagedResultDto<RoleDto>> GetAllAsync(CharismaticBaseListInputDto input)
        {
            return base.GetAllAsync(input);
        }

        public override async Task<RoleDto> GetAsync(EntityDto<int> input)
        {
            var role = await Repository.FirstOrDefaultAsync(input.Id);
            if (role == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Role));

            var roleDto = ObjectMapper.Map<RoleDto>(role);

            roleDto.UsersCount = await GetUsersCountForRole(new GetRolesInput { Id = roleDto.Id }); ;

            roleDto.GrantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(roleDto.Id)).Select(p => L("Permission_" + p.Name)).ToList();

            if (roleDto.CreatorUserId.HasValue)
                roleDto.CreatorUserName = (await _userManager.GetUserByIdAsync(roleDto.CreatorUserId.Value)).UserName;

            return roleDto;
        }

        public async Task ActivateDeactivate(EntityDto<int> input)
        {

            var role = await Repository.FirstOrDefaultAsync(input.Id);
            if (role == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Role));

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            //if (users.Count > 0)
            //    throw new UserFriendlyException(string.Format((Exceptions.UserAlreadyHasThisRole)));

            role.IsActive = !role.IsActive;

            MapToEntityDto(role);
        }

       // [CharismaticAuthorize(PermissionNames.Roles_View, RequireAllPermissions = true)]
        public async Task<PagedResultDto<RoleListResultDto>> GetAllRoles(CharismaticBaseListInputDto input)
        {
            var data = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(data);
            data = ApplyPaging(data, input);
            data = ApplySorting(data, input);


            var list = await AsyncQueryableExecuter.ToListAsync(data);

            list = list.WhereIf(input.IsActive.HasValue && input.IsActive.Value, i => i.IsActive).ToList();

            var ListDtos = ObjectMapper.Map<List<RoleListResultDto>>(list);

            foreach (var item in ListDtos)
            {
                if (input.IsActive.HasValue && input.IsActive.Value)
                    continue;

                item.GrantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(item.Id)).Select(p => p.Name).ToList();
                if (item.CreatorUserId.HasValue)
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(item.CreatorUserId.Value)).UserName;
                else
                    item.CreatorUserName = "";
                item.UsersCount = await GetUsersCountForRole(new GetRolesInput { Id = item.Id });
                item.CanDelete = !(item.UsersCount > 0);

            }

            return new PagedResultDto<RoleListResultDto>(totalCount, ListDtos);
        }

        protected override IQueryable<Role> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            //var data = Repository.GetAll();
            var data = base.CreateFilteredQuery(input);
            if (input.HasFilter)
                data = new DataSourceLoaderImpl<Role>(data, input, default, true).LoadAsync().Result;

            return data;
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }



        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        public Task<PagedResultDto<RoleDto>> GetAllAsync(PagedRoleResultRequestDto input)
        {
            throw new System.NotImplementedException();
        }
    }
}

