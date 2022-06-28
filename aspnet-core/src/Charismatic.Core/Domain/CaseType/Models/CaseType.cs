using Abp.Domain.Entities.Auditing;
using Charismatic.Domain.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class CaseType : FullAuditedEntity
    {
        public int? Type { get; set; }
        public virtual ICollection<Case> Cases { get; set; }        
        public virtual ICollection<CaseTypeProduct> CaseTypeProducts { get; set; }
        public virtual ICollection<CaseTypeDepartmentWorkFlow> CaseTypeDepartmentWorkFlows { get; set; }


    }
}
