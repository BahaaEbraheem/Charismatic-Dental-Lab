using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Departments.Dto
{
    [AutoMapFrom(typeof(Department))]
   public class DepartmentDto : EntityDto<int>
    {
        [Required]
        public string Name { get; set; }
        public int? SuperVisorId { get; set; }

        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
