using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Charismatic.Authorization.Users;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Doctors.Dto
{
    [AutoMapFrom(typeof(Doctor))]

   public class CreateDoctorDto 
    {
        [Required]
        public long UserId { get; set; }
        public string PhoneNumber { get; set; }
        public Status DoctorStatus { get; set; } = Status.Suspended;
        public string ClinicPhone { get; set; }
        public string ClinicEmail { get; set; }
        public string ResponsipleName { get; set; }
        public string ResponsiplePhone { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string Password { get; set; }

    }
}
