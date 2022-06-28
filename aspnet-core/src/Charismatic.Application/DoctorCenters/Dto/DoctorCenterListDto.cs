using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.DoctorCenters.Dto
{
    [AutoMap(typeof(DoctorCenter))]
    public class DoctorCenterListDto : EntityDto<int>
    {
        public int CenterId { get; set; }

        public int DoctorId { get; set; }
        public long? CreatorUserId { get; set; }

        public string CreatorUserName { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
