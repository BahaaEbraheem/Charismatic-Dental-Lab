using Abp.Authorization;
using Charismatic.Authorization.Roles;
using Charismatic.Authorization.Users;

namespace Charismatic.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
