using Charismatic.Products;
using Charismatic.Controllers;
using Charismatic.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Web.Controllers
{
    public class ProductsController : CharismaticControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly EnumHelper _enumHelper;

        public ProductsController(IProductAppService productAppService, EnumHelper enumHelper)
        {
            _productAppService = productAppService;

            _enumHelper = enumHelper;
        }

        public IActionResult Index()
        {
            fillViewBag();
            return View();
        }


        public void fillViewBag()
        {
            ViewBag.closingStatuses = _enumHelper.GetEnumAsList(typeof(ClosingStatus).ToString()).Select(i => Json(new
            {
                Value = i.Value,
                Text = i.Text
            })).ToList();
        }
        //public async Task<ActionResult> EditModal(int roleId)
        //{
        //    var output = await _roleAppService.GetRoleForEdit(new EntityDto(roleId));
        //    var model = ObjectMapper.Map<EditRoleModalViewModel>(output);

        //    return PartialView("_EditModal", model);
        //}
    }
}