using Charismatic.CaseTypes;
using Charismatic.Controllers;
using Charismatic.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Web.Controllers
{
    public class CaseTypesController : CharismaticControllerBase
    {
        private readonly ICaseTypesAppService _caseTypesAppService;
        private readonly EnumHelper _enumHelper;

        public CaseTypesController(ICaseTypesAppService caseTypesAppService, EnumHelper enumHelper)
        {
            _caseTypesAppService = caseTypesAppService;

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