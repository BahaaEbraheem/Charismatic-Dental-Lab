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
    public class EmployeesController : CharismaticControllerBase
    {
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IDepartmentAppService _departmentAppService;
        public EmployeesController(IEmployeeAppService employeeAppService, IDepartmentAppService departmentAppService)
        {
            _employeeAppService = employeeAppService;
            _departmentAppService = departmentAppService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.departments = (await _departmentAppService.GetAllDepartmentsAsync(new CharismaticBaseListInputDto()
            {
                MaxResultCount = int.MaxValue,
            })).Items.Select(i => Json(new
            {
                Value = i.Id,
                Text = i.Name
            })).ToList();
            return View();
        }
  
      


        //public async Task<ActionResult> EditModal(int roleId)
        //{
        //    var output = await _roleAppService.GetRoleForEdit(new EntityDto(roleId));
        //    var model = ObjectMapper.Map<EditRoleModalViewModel>(output);

        //    return PartialView("_EditModal", model);
        //}
    }
}