using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Charismatic.Authorization;
using Charismatic.Authorization.Accounts;
using Charismatic.Authorization.Roles;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.Localization.SourceFiles;
using Charismatic.Roles.Dto;
using Charismatic.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Charismatic.Users
{
    ///[AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : CharismaticAsyncCrudAppService<User, UserDto, long, CharismaticBaseListInputDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
        }


        public async Task<PagedResultDto<UserListDto>> GetAll(CharismaticBaseListInputDto input)
        {
            var data = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(data);
            data = ApplySorting(data, input);
            data = ApplyPaging(data, input);

            var list = await AsyncQueryableExecuter.ToListAsync(data);

            var ListDtos = ObjectMapper.Map<List<UserListDto>>(list);
         

            return new PagedResultDto<UserListDto>(totalCount, ListDtos);
        }
        [RemoteService(isEnabled: false)]
        public override Task<PagedResultDto<UserDto>> GetAllAsync(CharismaticBaseListInputDto input)
        {
            return base.GetAllAsync(input);
        }


        /// <summary>
        /// Assign  Role to user.
        /// </summary>

        public async Task<UserRoleDto> AssignRoleToUser(CreateUserRoleDto input)
        {
            //check if user exists
            var user = await Repository.FirstOrDefaultAsync(input.UserId);
            if (user == null)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, input.UserId));

            if (await _userManager.CheckUserRole(input.UserId, input.RoleId))
                throw new UserFriendlyException(string.Format(Exceptions.UserAlreadyHasThisRole));

       
            if (!(await _userManager.GetRoleAsync(input.RoleId)).IsActive)
                throw new UserFriendlyException(string.Format(Exceptions.ObjectWasNotFound, Tokens.Role));

            var userRole = ObjectMapper.Map<UserRole>(input);
            await _userManager.AssignRoleToUser(userRole);
            var userRoleDto = ObjectMapper.Map<UserRoleDto>(userRole);
            userRoleDto.CreaterUserName = (await _userManager.GetUserByIdAsync(userRole.CreatorUserId.Value)).UserName;
            userRoleDto.RoleName = (await _userManager.GetRoleAsync(userRole.RoleId)).Name;
            return userRoleDto;


        }

        /// <summary>
        /// De-assign  Role to user.
        /// </summary>
        public async System.Threading.Tasks.Task UnAssignRoleToUser(int id)
        {
            await _userManager.UnAssignRoleToUser(id);
        }



        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, CharismaticBaseListInputDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }

            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }

            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }


        public async Task<UserDto> GetUserDetails(long userId)
        {
            //get user details
            var user = ObjectMapper.Map<UserDto>(await Repository.FirstOrDefaultAsync(userId));
          

            //get user roles 
            user.Roles = ObjectMapper.Map<List<UserRoleDto>>(await _userManager.GetUserRolesAsync(userId));
            foreach (var userRole in user.Roles)
            {
                userRole.RoleName = (await _userManager.GetRoleAsync(userRole.RoleId)).Name;

                if (userRole.CreatorUserId.HasValue)
                    userRole.CreaterUserName = (await _userManager.GetUserByIdAsync(userRole.CreatorUserId.Value)).UserName;
             
            }



            return user;

        }
    }
}

