using Abp.Domain.Entities.Auditing;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class Center : FullAuditedAggregateRoot
    {
        [Required]
        public string Name { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
      
        [ForeignKey(nameof(Country))]
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public string AccountEmail { get; set; }

        [ForeignKey(nameof(State))]
        public int? StateId { get; set; }
        public virtual State State { get; set; }

        public virtual ICollection<DoctorCenter> DoctorCenters { get; set; }
    }
}
