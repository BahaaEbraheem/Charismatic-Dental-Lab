using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class DoctorCenter : FullAuditedEntity
    {
        [ForeignKey(nameof(Center))]
        public int CenterId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }

        public virtual Center Center { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
