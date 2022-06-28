using Charismatic.Cases;
using Charismatic.Addresses.CountriesService;
using Charismatic.Centers;
using Charismatic.Controllers;
using Charismatic.PatientReferrais;
using Charismatic.PatientReferrais.Dto;
using Charismatic.Web.Models.Cases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Charismatic.Enums;
using Abp.Runtime.Validation;
using Charismatic.Addresses.Dto.CountryDto;
using Abp.Runtime.Security;
using Charismatic.Doctors;
using Abp.AspNetCore.Mvc.Authorization;
using Charismatic.Cases.Dto;
using Charismatic.Products;
using Charismatic.Centers.Dto;

namespace Charismatic.Web.Controllers
{
    [AbpMvcAuthorize]
    public class CasesController : CharismaticControllerBase
    {
        private readonly IPatientReferraisAppService _patientReferraisAppService;
        private readonly ICenterAppService _centerAppService;
        private readonly IDoctorAppService _doctorAppService;
        private readonly ICountriesAppService _countriesAppService;
        private readonly ICasesAppService _caseAppService;
        private readonly IProductAppService _productAppService;

        public CasesController(IPatientReferraisAppService patientReferraisAppService,
            IDoctorAppService doctorAppService,
            ICenterAppService centerAppService,
            ICountriesAppService countriesAppService,
            ICasesAppService casesAppService,
            IProductAppService productAppService
            )
        {
            _patientReferraisAppService = patientReferraisAppService;
            _centerAppService = centerAppService;
            _doctorAppService = doctorAppService;
            _countriesAppService = countriesAppService;
            _caseAppService = casesAppService;
            _productAppService = productAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreatePrivateCase()
        {
            var model = new CreatePrivateCaseViewModel();
            var countries = await _countriesAppService.GetAllAsync(new GetAllCountriesInput());
            
            model.Countries =new SelectList(countries.Items.ToList(), "Id","Name");
            return View(model);
        }
        [HttpPost]        
        public async Task<IActionResult> CreatePrivateCase(CreatePrivateCaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var createPatientReferraisDto = ObjectMapper.Map<CreatePatientReferraisDto>(model);
                var caseDto=await _caseAppService.AddPrivateCaseStepOne(createPatientReferraisDto);
                return RedirectToAction("ChooseEvaluation", new { caseId= caseDto.Id });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> CreateCenterCase()
        {
            var model = new CreateCenterCaseViewModel();
            List<CenterListDto> centersList = null;// await _centerAppService.GetAllCentersForCurrentUserAsync(AbpSession.UserId);
            //model.Centers =  new SelectList(centersList.ToList(), "Id", "Name");


            
            var doc = (await _doctorAppService.GetAllDoctorsAsync(new CharismaticBaseListInputDto()
            {
                MaxResultCount = int.MaxValue,
            })).Items.FirstOrDefault(a => a.UserId == AbpSession.UserId);
            if (doc != null)
            {
               
                model.Centers = new SelectList((await _centerAppService.GetAllCentersForCurrentUserAsync()).Items, "Id", "Name"); ;
            }
            else
            {
                model.Centers = new SelectList(await _centerAppService.GetAll(), "Id", "Name");  
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCenterCase(CreateCenterCaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createPatientReferraisDto = ObjectMapper.Map<CreatePatientReferraisDto>(model);
                var caseDto = await _caseAppService.AddCenterCaseStepOne(createPatientReferraisDto, model.CenterId);
                return RedirectToAction("ChooseEvaluation", new { caseId= caseDto.Id });
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult ChooseEvaluation(int caseId)
        {
            var model = new ChooseEvaluationViewModel { CaseId=caseId};
            return View(new ChooseEvaluationViewModel { CaseId = caseId });
        }

        [HttpPost]
        public async Task<IActionResult> ChooseEvaluationSet(ChooseEvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var chooseEvaluationDto= ObjectMapper.Map<ChooseEvaluationDto>(model);
                var dto = await _caseAppService.AddCaseStepTwo(chooseEvaluationDto);
                return RedirectToAction("ChooseProducts", new { caseId = dto.Id });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> ChooseProducts(int caseId)
        {
            var products = await _productAppService.GetAllForChoose();
            var model = new ChooseProductViewModel { CaseId=caseId,Products=products.Items.ToList()};
            return View(model);
        }
    }
}
