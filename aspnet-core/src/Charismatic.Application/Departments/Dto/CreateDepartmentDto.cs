using Abp.AutoMapper;
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

    public class CreateDepartmentDto
    {
        [Required]
        public string Name { get; set; }
        public int? SuperVisorId { get; set; }

    }
}
