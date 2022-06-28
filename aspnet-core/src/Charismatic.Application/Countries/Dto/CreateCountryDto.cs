using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Countries.Dto
{
    [AutoMap(typeof(Country))]
    public class CreateCountryDto
    {
        [Required]
        [StringLength(600)]
        public string Name { get; set; }
    }
}
