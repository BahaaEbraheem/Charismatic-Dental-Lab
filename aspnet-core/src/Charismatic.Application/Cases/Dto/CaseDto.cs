using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Cases.Dto
{
    [AutoMap(typeof(Case))]
    public  class CaseDto : EntityDto<int>
    {
        public int? PatientReferraisId { get; set; }
        public int? DoctorId { get; set; }        
        public Charismatic.Enums.CaseType Type { get; set; }
        public CaseStatus CaseStatus { get; set; }
    }
}
