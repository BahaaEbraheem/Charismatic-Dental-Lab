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
    public class Admin : FullAuditedEntity
    {
        [Required]
        public long UserId { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public virtual ICollection<Department> Departments { get; set; }

    }
}
