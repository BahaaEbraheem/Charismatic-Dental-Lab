using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Charismatic.Web.Views
{
    public abstract class CharismaticRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected CharismaticRazorPage()
        {
            LocalizationSourceName = CharismaticConsts.LocalizationSourceName;
        }
    }
}
