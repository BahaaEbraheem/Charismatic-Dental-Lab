using Abp.AutoMapper;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Missions.Dto
{
    [AutoMapFrom(typeof(Mission))]

    public class CreateMissionDto
    {
        [Required(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = nameof(DataAnnotations.Required))]
        [StringLength(600, ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = nameof(DataAnnotations.StringLength))]
        public string Name { get; set; }
        public int? CaseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MissionStatus State { get; set; }

    }
}
