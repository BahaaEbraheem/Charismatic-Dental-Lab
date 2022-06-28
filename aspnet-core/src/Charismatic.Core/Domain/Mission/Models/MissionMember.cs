using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Models
{
   public class MissionMember : FullAuditedEntity
    {
        [ForeignKey(nameof(Mission))]
        public int? MissionId { get; set; }
        public virtual Mission Mission { get; set; }

        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EmployeeStatus State { get; set; }

        public bool IsRunning { get; set; }

        public int OrderEmp { get; set; }
    }
}
