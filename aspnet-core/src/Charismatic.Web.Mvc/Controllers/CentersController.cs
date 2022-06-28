using Charismatic.Addresses.CountriesService;
using Charismatic.Addresses.Dto.CountryDto;
using Charismatic.Centers;
using Charismatic.Centers.Dto;
using Charismatic.Controllers;
using Charismatic.Countries;
using Charismatic.Web.Models.Centers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic.Web.Controllers
{
    public class CentersController : CharismaticControllerBase
    {
        private readonly ICenterAppService _centerAppService;
        private readonly ICountryAppService _countryAppService;
        private readonly ICountriesAppService _countriesAppService;
        public CentersController(ICenterAppService centerAppService, ICountryAppService countryAppService, ICountriesAppService countriesAppService)
        {
            _centerAppService = centerAppService;
            _countryAppService = countryAppService;
            _countriesAppService = countriesAppService;
        }

        public async Task<IActionResult> IndexAsync()
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateCenterViewModel();
            var countries = await _countriesAppService.GetAllAsync(new GetAllCountriesInput());

            model.Countries = new SelectList(countries.Items.ToList(), "Id", "Name");
            return PartialView("_CreateCenter", model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateCenterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var createCenterDto = ObjectMapper.Map<CreateCenterDto>(model);
                var centerDto= await _centerAppService.CreateAsync(createCenterDto);
                return Ok(centerDto);
            }
            else
            {
                return View(model);
            }
        }
    }
}