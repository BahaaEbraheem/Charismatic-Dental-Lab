using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic.Web.Views.Shared.Components.ContentHeader
{
    public class ContentHeaderViewComponent : CharismaticViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string pageName = "")
        {
            var model = new ContentHeaderViewModel
            {
                PageName = pageName,
            };
            return View(model);
        }
    }
}