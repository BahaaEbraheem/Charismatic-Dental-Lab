using Abp.AutoMapper;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.CaseTypes.Dto
{
    [AutoMap(typeof(CaseType))]
    public class CreateCaseTypeDto
    {
        [Required]
        
        public int Type { get; set; }
    }
}
