using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Departments.Dto
{
    [AutoMapFrom(typeof(Department))]

   public class EditDepartmentDto : CreateDepartmentDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
