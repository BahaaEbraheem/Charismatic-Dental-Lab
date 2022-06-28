using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Cases.Dto
{
    public class ChooseEvaluationDto
    {
        public int CaseId { get; set; }
        public CaseEvaluationType EvaluationType { get; set; }
    }
}
