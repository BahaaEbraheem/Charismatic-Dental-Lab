using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class AdvancedA : FullAuditedEntity
    {
        public int? Inclination { get; set; }

        public int? SmileLine { get; set; }

        public int? SmileWidth { get; set; }

        public int? LabiaCorridor { get; set; }

        [ForeignKey(nameof(Case))]
        public int CaseId { get; set; }

        public virtual Case Case { get; set; }

    }
}
