using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Charismatic.Authorization;
using Charismatic.Authorization.Roles;
using Charismatic.Authorization.Users;

namespace Charismatic.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly CharismaticDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(CharismaticDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // Doctor role 

            var DoctorRoleForTenant = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Doctor);
            if (DoctorRoleForTenant == null)
            {
                DoctorRoleForTenant = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Doctor, StaticRoleNames.Tenants.Doctor) { IsStatic = true, IsActive = true }).Entity;
                _context.SaveChanges();
            }

            // Grant Doctor permission to Doctor role
            var DoctorPermission = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .FirstOrDefault(p => p.TenantId == _tenantId && p.RoleId == DoctorRoleForTenant.Id && p.IsGranted == true);
            if (DoctorPermission == null)
            {
                _context.Permissions.Add(new RolePermissionSetting() { TenantId = _tenantId, IsGranted = true, RoleId = DoctorRoleForTenant.Id, Name = PermissionNames.Pages_Doctors });
            }


            // Admin role

            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true,IsActive=true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to admin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new CharismaticAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));

                _context.SaveChanges();
            }
        }
    }
}
