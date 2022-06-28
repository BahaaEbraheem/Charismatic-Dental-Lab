using Abp.Domain.Entities.Auditing;
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

namespace Charismatic.Models
{
    public class Doctor : FullAuditedEntity
    {
        [Required]
        public  long UserId { get; set; }
        public Status DoctorStatus { get; set; } = Status.Suspended;
        public string PhoneNumber { get; set; }
        public string ClinicPhone { get; set; }
        public string ClinicEmail { get; set; }
        public string ResponsipleName { get; set; }
        public string ResponsiplePhone { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
        public virtual ICollection<DoctorCenter> DoctorCenters { get; set; }
        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
        

     


    }
}
