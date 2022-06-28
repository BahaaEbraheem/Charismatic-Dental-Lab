using Abp.Domain.Repositories;
using Charismatic.CrudAppServiceBase;
using Charismatic.Domain.PatientReferrais;
using Charismatic.PatientReferrais.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.PatientReferrais
{
    public class PatientReferraisAppService: CharismaticAsyncCrudAppService<Charismatic.Models.PatientReferrais, PatientReferraisDto, int, CharismaticBaseListInputDto, CreatePatientReferraisDto, EditPatientReferraisDto>,IPatientReferraisAppService
    {
        private readonly IPatientReferraisManager _patientReferraisManager;
        public PatientReferraisAppService(IRepository<Charismatic.Models.PatientReferrais, int> repository, IPatientReferraisManager patientReferraisManager) : base(repository)
        {
            _patientReferraisManager = patientReferraisManager;
        }


        public override async Task<PatientReferraisDto> CreateAsync(CreatePatientReferraisDto input)
        {
            var patient = MapToEntity(input);
            patient.Id = await Repository.InsertAndGetIdAsync(patient);
            var patientDto = MapToEntityDto(patient);
            return patientDto;
        }
    }
}
