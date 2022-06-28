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
    [AutoMapTo(typeof(Case))]
    public class CreateCaseInput
    {
        public int? PatientReferraisId { get; set; }
        public Charismatic.Enums.CaseType Type { get; set; }
        public CaseStatus CaseStatus { get; set; }
        public int? DoctorId { get; set; }
        public string Name { get; set; }
        public int? CenterId { get; set; }
    }
}
