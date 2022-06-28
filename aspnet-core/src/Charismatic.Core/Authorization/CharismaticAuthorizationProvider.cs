using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using Charismatic.Localization.SourceFiles;

namespace Charismatic.Authorization
{
    public class CharismaticAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Pages_Doctors, L("Doctors"));
            context.CreatePermission(PermissionNames.Pages_Employees, L("Employees"));
            context.CreatePermission(PermissionNames.Pages_Specialties, L("Specialties"));
            context.CreatePermission(PermissionNames.Pages_Centers,L("Centers"));
            context.CreatePermission(PermissionNames.Pages_Admins, L("Admins"));



            context.CreatePermission(PermissionNames.Doctors_View, L("Doctors_View"));
            context.CreatePermission(PermissionNames.Doctors_Active_Deactive, L("Doctors_Active_Deactive"));
            context.CreatePermission(PermissionNames.Doctors_Add_Edit, L("Doctors_Add_Edit"));

            context.CreatePermission(PermissionNames.Employees_View, L("Employees_View"));
            context.CreatePermission(PermissionNames.Employees_Add_Edit, L("Employees_Add_Edit"));

            context.CreatePermission(PermissionNames.Missions_View, L("Missions_View"));
            context.CreatePermission(PermissionNames.Missions_ChangeState, L("Missions_ChangeState"));






        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CharismaticConsts.LocalizationSourceName);
        }
    }
}
