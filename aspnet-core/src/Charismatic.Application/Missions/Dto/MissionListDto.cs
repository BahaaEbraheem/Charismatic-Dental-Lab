using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Missions.Dto
{
    [AutoMapFrom(typeof(Mission))]

   public class MissionListDto : EntityDto<int>
    {
        [Required]
        public string Name { get; set; }
        public int? CaseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MissionStatus State { get; set; }
        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
        public string DoctorName { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public virtual ICollection<MissionMemberDto> MissionMembersDto { get; set; }

        public int? CurrentEmployeeId { get; set; }
    }
}
