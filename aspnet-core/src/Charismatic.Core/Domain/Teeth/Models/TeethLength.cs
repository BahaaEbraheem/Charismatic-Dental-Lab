using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class TeethLength : FullAuditedEntity
    {
        public int? UpperRightLength { get; set; }

        public int? UpperRightLateredLength { get; set; }
        
       public int? UpperRightCanineLength { get; set; }

    }
}
