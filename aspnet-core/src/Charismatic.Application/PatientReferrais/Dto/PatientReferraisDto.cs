using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using static Charismatic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.PatientReferrais.Dto
{
    [AutoMap(typeof(Charismatic.Models.PatientReferrais))]
    public class PatientReferraisDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string BuildingName { get; set; }
        public string Country { get; set; }
        public Gender? Gender { get; set; }
    }
}
