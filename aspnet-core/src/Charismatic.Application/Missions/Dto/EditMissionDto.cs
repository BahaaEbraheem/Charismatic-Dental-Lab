using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Missions.Dto
{
    [AutoMapFrom(typeof(Mission))]
    public class EditMissionDto : CreateMissionDto, IEntityDto<int>
    {

        public int Id { get; set; }
    }
}
