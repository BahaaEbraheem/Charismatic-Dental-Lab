using Abp.Domain.Entities.Auditing;
using Charismatic.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class Employee : FullAuditedEntity
    {

        [Required]
        public long UserId { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public string Code { get; set; }
        public virtual ICollection<MissionMember> MissionMembers { get; set; }

    }
}
