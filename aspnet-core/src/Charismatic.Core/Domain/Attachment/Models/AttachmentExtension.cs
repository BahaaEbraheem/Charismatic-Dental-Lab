using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class AttachmentExtension : FullAuditedEntity
    {
        [ForeignKey(nameof(AttachmentType))]
        public int? AttachmentTypeId { get; set; }

        [ForeignKey(nameof(Extension))]
        public int? ExtensionId { get; set; }

        public int? MaxSize { get; set; }

        public virtual AttachmentType AttachmentType { get; set; }

        public virtual Extension Extension { get; set; }
    }
}
