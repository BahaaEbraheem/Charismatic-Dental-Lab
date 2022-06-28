using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class EvaluationCase : FullAuditedEntity
    {
        public int Type { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
