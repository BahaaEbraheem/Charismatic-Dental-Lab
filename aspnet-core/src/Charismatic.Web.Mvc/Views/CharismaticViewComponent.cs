using Abp.AspNetCore.Mvc.ViewComponents;

namespace Charismatic.Web.Views
{
    public abstract class CharismaticViewComponent : AbpViewComponent
    {
        protected CharismaticViewComponent()
        {
            LocalizationSourceName = CharismaticConsts.LocalizationSourceName;
        }
    }
}
