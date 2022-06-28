using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class AdvancedB : FullAuditedEntity
    {
        public double?  LowerTeeth { get; set; }

        public double? FaceMidline { get; set; }

        public double? UpperTeeth { get; set; }

        [ForeignKey(nameof(Case))]
        public int CaseId { get; set; }

        public virtual Case Case { get; set; }
    }
}
