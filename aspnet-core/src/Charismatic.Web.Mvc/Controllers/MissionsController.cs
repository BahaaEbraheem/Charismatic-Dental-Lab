using Charismatic.Addresses.CountriesService;
using Charismatic.Addresses.Dto.CountryDto;
using Charismatic.Cases;
using Charismatic.Centers;
using Charismatic.Centers.Dto;
using Charismatic.Controllers;
using Charismatic.Countries;
using Charismatic.Missions;
using Charismatic.Web.Models.Centers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Web.Controllers
{
    public class MissionsController : CharismaticControllerBase
    {
        private readonly ICasesAppService _casesAppService;
        private readonly EnumHelper _enumHelper;
        public MissionsController(ICasesAppService casesAppService, EnumHelper enumHelper)
        {
            _casesAppService = casesAppService;
            _enumHelper = enumHelper;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.cases = ( _casesAppService.GetAllCases(new CharismaticBaseListInputDto()
            {
                MaxResultCount = int.MaxValue,
            })).Items.Select(i => Json(new
            {
                Value = i.Id,
                Text = i.CaseNumber
            })).ToList();

            fillViewBag();
            return View();
        }


        public void fillViewBag()
        {
            ViewBag.missionStates = _enumHelper.GetEnumAsList(typeof(MissionStatus).ToString()).Select(i => Json(new
            {
                Value = i.Value,
                Text = i.Text
            })).ToList();

            ViewBag.employeesStates = _enumHelper.GetEnumAsList(typeof(EmployeeStatus).ToString()).Select(i => Json(new
            {
                Value = i.Value,
                Text = i.Text
            })).ToList();

            
        }
      
    }
}