using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using static Charismatic.Enums;

namespace Charismatic.Missions.Dto
{
    [AutoMapFrom(typeof(MissionMember))]


    public class MissionMemberDto : EntityDto<int>
    {
        
        public int? MissionId { get; set; }

        public int? EmployeeId { get; set; }
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EmployeeStatus State { get; set; }

        public bool IsRunning { get; set; }

        public int OrderEmp { get; set; }
    }
}
