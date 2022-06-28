using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class Department : FullAuditedAggregateRoot
    {
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Admin))]
        public int? SuperVisorId { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<CaseTypeDepartmentWorkFlow> CaseTypeDepartmentWorkFlows { get; set; }
    }
}
