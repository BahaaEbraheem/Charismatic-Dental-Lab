using Abp.Domain.Entities.Auditing;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Models
{
    public class PatientReferrais : FullAuditedAggregateRoot
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
        public virtual ICollection<Case> Cases { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }

        [ForeignKey(nameof(StateId))]
        public virtual State State { get; set; }
    }
}
