using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Charismatic.Authorization.Roles;
using System.Threading.Tasks;
using System.Linq;

namespace Charismatic.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {
        public IRepository<Role, int> _roleRepository;
        public readonly IRepository<User, long> _userRepository;
        public IRepository<UserRole, long> _userRoleRepository;
        public UserManager(

            RoleManager roleManager,
            UserStore store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, 
            IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<User>> logger, 
            IPermissionManager permissionManager, 
            IUnitOfWorkManager unitOfWorkManager, 
            ICacheManager cacheManager, 
            IRepository<OrganizationUnit, long> organizationUnitRepository, 
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository, 
            IOrganizationUnitSettings organizationUnitSettings,
            IRepository<Role, int> roleRepository,
            IRepository<User, long> userRepository,
             IRepository<UserRole, long> userRoleRepository,
            ISettingManager settingManager)
            : base(
                roleManager, 
                store, 
                optionsAccessor, 
                passwordHasher, 
                userValidators, 
                passwordValidators, 
                keyNormalizer, 
                errors, 
                services, 
                logger, 
                permissionManager, 
                unitOfWorkManager, 
                cacheManager,
                organizationUnitRepository, 
                userOrganizationUnitRepository, 
                organizationUnitSettings, 
                settingManager)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Get a specific Role.
        /// </summary>
        public async Task<Role> GetRoleAsync(long roleId)
        {
            return await _roleRepository.FirstOrDefaultAsync(i => i.Id == roleId);
        }

        /// <summary>
        /// Check if user has a specific role.
        /// </summary>
        public async Task<bool> CheckUserRole(long userId, int roleId)
        {

            //get user roles which have a specific role and belong to a specific user.
            var userRoles = await _userRoleRepository.GetAllListAsync(p => p.RoleId == roleId && p.UserId == userId);

            if (userRoles.Count() <= 0)
                return false;

            return true;

        }

        /// <summary>
        ///Assign role to user.
        /// </summary>
        public async Task<UserRole> AssignRoleToUser(UserRole userRole)
        {
            userRole.Id = await _userRoleRepository.InsertAndGetIdAsync(userRole);
            return userRole;
        }

        /// <summary>
        ///De-assign type to user.
        /// </summary>
        public async System.Threading.Tasks.Task UnAssignRoleToUser(int id)
        {
            await _userRoleRepository.DeleteAsync(id);
        }


        /// <summary>
        /// Get Roles for a specific user .
        /// </summary>
        public async Task<List<UserRole>> GetUserRolesAsync(long userId)
        {
            return (await _userRoleRepository.GetAllListAsync(x => x.UserId == userId));

        }
    }

}
