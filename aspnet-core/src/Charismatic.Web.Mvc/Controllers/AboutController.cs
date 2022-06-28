using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Charismatic.Controllers;

namespace Charismatic.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : CharismaticControllerBase
    {
        public ActionResult Index()
        {
            //test
            return View();
        }
	}
}
