using Abp.AutoMapper;
using Charismatic.Cases.Dto;
using System.ComponentModel.DataAnnotations;
using static Charismatic.Enums;

namespace Charismatic.Web.Models.Cases
{
    [AutoMapTo(typeof(ChooseEvaluationDto))]
    public class ChooseEvaluationViewModel
    {
        [Required]
        public int? CaseId{ get; set; }
        [Required]
        public CaseEvaluationType? EvaluationType { get; set; }
    }
}
