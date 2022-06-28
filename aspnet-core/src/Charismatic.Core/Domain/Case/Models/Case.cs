using Abp.Domain.Entities.Auditing;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Models
{
    public class Case : FullAuditedAggregateRoot
    {
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(PatientReferrais))]
        public int? PatientReferraisId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }


        public int?  LevelUpperLipRElationToIncisors { get; set; }

        public double? AmontIncisorsOnRestPostionUpper { get; set; }

        public double? AmontIncisorsOnRestPostionLower { get; set; }


        public double? DesiredTeethPropWidthToHeight { get; set; }


        public bool? IsSpecific { get; set; }

        public DateTime StartDate { get; set; }

        public bool? PhydicalImpresssion { get; set; }

        public bool? DigitalImpression { get; set; }


        [ForeignKey(nameof(CaseType))]
        public int? TypeId { get; set; }

        [ForeignKey(nameof(EvaluationCase))]
        public int? EvaluationCaseId { get; set; }


        [InverseProperty("Case")]
        public virtual AdvancedA AdvancedA { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual PatientReferrais PatientReferrais { get; set; }


        [InverseProperty("Case")]
        public virtual AdvancedB AdvancedB { get; set; }

        public virtual CaseType CaseType { get; set; }

        public virtual EvaluationCase EvaluationCase { get; set; }
        
        public virtual ICollection<TeethCase> TeethCases { get; set; }

        public virtual ICollection<CaseProduct> CaseProducts { get; set; }

        public Charismatic.Enums.CaseType Type { get; set; }
        public CaseStatus  CaseStatus { get; set; }
        public CaseEvaluationType? CaseEvaluationType { get; set; }
        public virtual ICollection<Mission> Missions { get; set; }


    }
}
