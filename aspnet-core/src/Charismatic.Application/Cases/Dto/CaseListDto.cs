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
    [AutoMapFrom(typeof(Case))]
    public  class CaseListDto : EntityDto<int>
    {
        public string CaseNumber { get; set; }
        public string DoctorName  { get; set; }
        public string PatientReferraisName { get; set; }
        public string Type { get; set; }
        public string CaseStatus { get; set; }
        public string CaseEvaluationType { get; set; }
    }
}
