using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class CaseDepartmentExcluded : FullAuditedEntity
    {
        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }

        public virtual Case Case { get; set; }

        [ForeignKey(nameof(CaseTypeDepartmentWorkFlowse))]
        public int? CaseTypeDepartmentWorkFlowId { get; set; }

        public virtual CaseTypeDepartmentWorkFlow CaseTypeDepartmentWorkFlowse { get; set; }
    }
}
