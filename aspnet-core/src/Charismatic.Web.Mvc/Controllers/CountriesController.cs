using Charismatic.Centers;
using Charismatic.Controllers;
using Charismatic.Countries;
using Charismatic.Specialties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic.Web.Controllers
{
    public class CountriesController : CharismaticControllerBase
    {
        private readonly ICountryAppService _countryAppService;

        public CountriesController(ICountryAppService countryAppService)
        {
            _countryAppService = countryAppService;
        }

        public  IActionResult Index()
        {

            return View();
        }

    }
}