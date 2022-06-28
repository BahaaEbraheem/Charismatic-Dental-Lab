using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.PatientReferrais.Dto
{
    [AutoMap(typeof(Charismatic.Models.PatientReferrais))]
    public class CreatePatientReferraisDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StreetAddress { get; set; }
        public string BuildingName { get; set; }
        public Gender Gender { get; set; }
        public int? CountryId { get; set; }

        public int? StateId { get; set; }
    }
}
