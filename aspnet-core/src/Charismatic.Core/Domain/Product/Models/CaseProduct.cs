using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
   public class CaseProduct  : FullAuditedAggregateRoot
    {
        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }

        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Case Case { get; set; }

    }
}
