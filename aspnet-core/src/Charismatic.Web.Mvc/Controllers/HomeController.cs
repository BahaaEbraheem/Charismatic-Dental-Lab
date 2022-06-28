using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Charismatic.Controllers;

namespace Charismatic.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : CharismaticControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
