using Charismatic.Centers;
using Charismatic.Controllers;
using Charismatic.Countries;
using Charismatic.Doctors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic.Web.Controllers
{
    public class DoctorsController : CharismaticControllerBase
    {
        private readonly IDoctorAppService _doctorAppService;
        private readonly ICenterAppService _centerAppService;
        private readonly ICountryAppService _countryAppService;
        public DoctorsController(IDoctorAppService doctorAppService, ICenterAppService centerAppService, ICountryAppService countryAppService)
        {
            _doctorAppService = doctorAppService;
            _centerAppService = centerAppService;
            _countryAppService = countryAppService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.countries = (await _countryAppService.GetAllCountriesAsync(new CharismaticBaseListInputDto()
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