using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using Charismatic.Models.Address;
using Charismatic.Specialties.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Countries.Dto
{
    [AutoMap(typeof(Country))]
    public class EditCountryDto : CreateCountryDto, IEntityDto<int>
    {
    
        public int Id { get; set; }

    }
}
