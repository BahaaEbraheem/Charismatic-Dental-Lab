using Charismatic.Admins;
using Charismatic.Centers;
using Charismatic.Controllers;
using Charismatic.Countries;
using Charismatic.Departments;
using Charismatic.Doctors;
using Charismatic.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic.Web.Controllers
{
    public class DepartmentsController : CharismaticControllerBase
    {
        private readonly IDepartmentAppService _departmentAppService;
        private readonly IAdminAppService _adminAppService;

        public DepartmentsController(IDepartmentAppService departmentAppService, IAdminAppService adminAppService)
        {
            _adminAppService = adminAppService;
            _departmentAppService = departmentAppService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.admins = (await _adminAppService.GetAllAdminsAsync(new CharismaticBaseListInputDto()
            {
                MaxResultCount = int.MaxValue,
            })).Items.Select(i => Json(new
            {
                Value = i.Id,
                Text = i.Name
            })).ToList();
            return View();
        }
    }
}