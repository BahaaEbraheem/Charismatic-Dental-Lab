using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class CaseTypeDepartmentWorkFlow : FullAuditedEntity
    {
        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(CaseType))]
        public int? CaseTypeId { get; set; }

        public int? order { get; set; }

        public virtual Department Department { get; set; }

        public virtual CaseType CaseType { get; set; }
    }
}
