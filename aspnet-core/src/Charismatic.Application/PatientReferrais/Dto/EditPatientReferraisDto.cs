using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.PatientReferrais.Dto
{
    [AutoMap(typeof(Charismatic.Models.PatientReferrais))]
    public class EditPatientReferraisDto:EntityDto
    {
    }
}
