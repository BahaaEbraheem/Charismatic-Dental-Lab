using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.Cases.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Cases
{
    public interface ICasesAppService: ICharismaticAsyncCrudAppService<CaseDto, int, CharismaticBaseListInputDto, CreateCaseInput, UpdateCaseInput>
    {

        PagedResultDto<CaseListDto> GetAllCases(CharismaticBaseListInputDto input);
        Task<CaseDto> AddPrivateCaseStepOne(PatientReferrais.Dto.CreatePatientReferraisDto PatientDto);
        Task<CaseDto> AddCenterCaseStepOne(PatientReferrais.Dto.CreatePatientReferraisDto PatientDto,int centerId);
        Task<CaseDto> AddCaseStepTwo(ChooseEvaluationDto dto);
    }
}
