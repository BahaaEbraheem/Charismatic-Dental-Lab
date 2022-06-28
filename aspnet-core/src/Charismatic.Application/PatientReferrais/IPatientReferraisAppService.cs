using Charismatic.CrudAppServiceBase;
using Charismatic.PatientReferrais.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.PatientReferrais
{
    public interface IPatientReferraisAppService : ICharismaticAsyncCrudAppService<PatientReferraisDto, int, CharismaticBaseListInputDto, CreatePatientReferraisDto, EditPatientReferraisDto>
    {
        Task<PatientReferraisDto> CreateAsync(CreatePatientReferraisDto input);
    }
}
