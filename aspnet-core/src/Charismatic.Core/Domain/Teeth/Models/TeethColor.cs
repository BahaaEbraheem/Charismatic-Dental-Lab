using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class TeethColor : FullAuditedEntity
    {
        public string Neck { get; set; }

        public string Body { get; set; }

        public string IncisalEdge { get; set; }

        public virtual ICollection<TeethCase> TeethCases { get; set; }
    }
}
