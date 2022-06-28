using Abp.Domain.Entities.Auditing;
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
  public class Mission : FullAuditedAggregateRoot
    {
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }
        public virtual Case Case { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MissionStatus State { get; set; }
       public virtual ICollection<MissionMember> MissionMembers { get; set; }





    }
}
