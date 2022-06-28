using Abp.Domain.Entities.Auditing;
using Charismatic.Domain.Product.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class Product : FullAuditedAggregateRoot
    {
        [Required]
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
        public virtual ICollection<CaseProduct> CaseProducts { get; set; }
        public virtual ICollection<CaseTypeProduct> CaseTypeProducts { get; set; }

    }
}
