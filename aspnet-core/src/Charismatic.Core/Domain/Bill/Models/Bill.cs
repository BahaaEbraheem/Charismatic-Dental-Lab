using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class Bill : FullAuditedAggregateRoot
    {
        public DateTime Date { get; set; }

        public DateTime PayDate {get;set;}

        public String Description { get; set; }
    }
}
