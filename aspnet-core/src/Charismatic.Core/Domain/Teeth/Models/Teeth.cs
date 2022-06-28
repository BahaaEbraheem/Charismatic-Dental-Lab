using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class Teeth : FullAuditedAggregateRoot
    {
        public string  Name { get; set; }
        public int Number { get; set; }

        public virtual ICollection<TeethCase> TeethCases { get; set; }
    }
}
