using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Domain.PatientReferrais
{
    public class PatientReferraisManager:DomainService,IPatientReferraisManager
    {
        private readonly IRepository<Models.PatientReferrais> _patientReferraisRepository;

        public PatientReferraisManager(IRepository<Charismatic.Models.PatientReferrais> patientReferraisRepository)
        {
            this._patientReferraisRepository = patientReferraisRepository;
        }
    }
}
