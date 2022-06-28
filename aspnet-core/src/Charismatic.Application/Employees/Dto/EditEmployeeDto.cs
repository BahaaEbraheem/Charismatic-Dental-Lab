using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Employees.Dto
{
    [AutoMapFrom(typeof(Employee))]

   public class EditEmployeeDto : CreateEmployeeDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
