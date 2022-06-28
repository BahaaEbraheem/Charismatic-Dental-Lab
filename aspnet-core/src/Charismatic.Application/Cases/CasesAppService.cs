using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Charismatic.Authorization.Users;
using Charismatic.Cases.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Domain.Case;
using Charismatic.Models;
using Charismatic.PatientReferrais;
using Charismatic.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Cases
{
    public class CasesAppService : CharismaticAsyncCrudAppService<Case, CaseDto, int, CharismaticBaseListInputDto, CreateCaseInput, UpdateCaseInput>, ICasesAppService
    {
        private readonly ICaseManager _caseManager;
        private readonly IPatientReferraisAppService _patientReferraisAppService;
        private readonly UserManager _userManager;
        public CasesAppService(IRepository<Case, int> repository, ICaseManager caseManager, IPatientReferraisAppService patientReferraisAppService, UserManager userManager) : base(repository)
        {
            _caseManager = caseManager;
            _patientReferraisAppService = patientReferraisAppService;
            _userManager = userManager;
        }
        public PagedResultDto<CaseListDto> GetAllCases(CharismaticBaseListInputDto input)
        {
            var cases = Repository.GetAllIncluding(c => c.PatientReferrais,c=>c.Doctor).ToList();
            return new PagedResultDto<CaseListDto>(cases.Count,
                    ObjectMapper.Map<List<CaseListDto>> (cases));
        }
        public async Task<CaseDto> AddPrivateCaseStepOne(PatientReferrais.Dto.CreatePatientReferraisDto PatientDto)
        {
            var patient = await _patientReferraisAppService.CreateAsync(PatientDto);
            var caseCount = Repository.Count();
            var caseName = "C" + string.Format("{0,5:D5}", caseCount+1);
            var createCaseInput = new  CreateCaseInput
            {
                PatientReferraisId = patient.Id,
                Type = Enums.CaseType.Private,
                CaseStatus = Enums.CaseStatus.New,
                DoctorId = (int?)AbpSession.UserId,
                Name= caseName
            };
            var caseDto = await CreateAsync(createCaseInput);
            return caseDto;
        }
        public async Task<CaseDto> AddCenterCaseStepOne(PatientReferrais.Dto.CreatePatientReferraisDto PatientDto, int centerId)
        {
            var patient = await _patientReferraisAppService.CreateAsync(PatientDto);
            var caseCount = Repository.Count();
            var caseName = "C" + string.Format("{0,5:D5}", caseCount + 1);
            var createCaseInput = new CreateCaseInput
            {
                PatientReferraisId = patient.Id,
                Type = Enums.CaseType.Center,
                CaseStatus = Enums.CaseStatus.New,
                DoctorId = (int?)AbpSession.UserId,
                Name = caseName,
                CenterId=centerId
            };
            var caseDto = await CreateAsync(createCaseInput);
            return caseDto;
        }

        public async  Task<CaseDto> AddCaseStepTwo(ChooseEvaluationDto dto)
        {
            var caseEntity = await Repository.GetAsync(dto.CaseId);
            caseEntity.CaseEvaluationType = dto.EvaluationType;
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(caseEntity);
        }
    }
}
