using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class TeethCase : FullAuditedEntity
    {
        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }

        [ForeignKey(nameof(Teeth))]
        public int  TeethId { get; set; }

        [ForeignKey(nameof(TeethColor))]
        public int?  ColorId { get; set; }

        public bool? IsImplant { get; set; }

        public bool?  IsMyreland { get; set; }

        public bool? IsPontics { get; set; }

        public bool? IsCrown { get; set; }

        public bool? IsVeneers { get; set; }

        public bool? IsPost { get; set; }

        public bool? IsDoubleCrown { get; set; }

        [ForeignKey(nameof(Matrial))]
        public int? MatrialId { get; set; }

        public int? Shape { get; set; }

        public int? Texture { get; set; }

        public int? LengthId { get; set; }

        public int? Translucence { get; set; }


        public int? StumpShade { get; set; }

        public string Instruction { get; set; }

        public virtual Case Case { get; set; }

        public virtual TeethColor TeethColor { get; set; }

        public virtual Teeth Teeth { get; set; }

        public virtual Matrial Matrial { get; set; }
        


    }
}
