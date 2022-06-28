using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Charismatic.Controllers
{
    public abstract class CharismaticControllerBase: AbpController
    {
        protected CharismaticControllerBase()
        {
            LocalizationSourceName = CharismaticConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
