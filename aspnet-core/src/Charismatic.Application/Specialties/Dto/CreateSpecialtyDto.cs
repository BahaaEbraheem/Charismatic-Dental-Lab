using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Specialties.Dto
{
    [AutoMap(typeof(Specialty))]
    public class CreateSpecialtyDto 
    {
        [Required]
        [StringLength(600)]
        public string Name { get; set; }
    }
}
