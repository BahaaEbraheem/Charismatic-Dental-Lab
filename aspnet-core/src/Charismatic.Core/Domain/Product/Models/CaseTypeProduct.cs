using Abp.Domain.Entities.Auditing;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Domain.Product.Models
{
    public class CaseTypeProduct: FullAuditedEntity
    {
        public int ProductId { get; set; }
        public int CaseTypeId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Charismatic.Models.Product Product { get; set; }

        [ForeignKey(nameof(CaseTypeId))]
        public virtual Charismatic.Models.CaseType CaseType { get; set; }
    }
}
