using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class AttachmentType : FullAuditedEntity
    {
        public string Name { get; set; }

        public int? MaxCount { get; set; }
    }
}
