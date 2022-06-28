using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Addresses.Dto.CountryDto
{
    [AutoMap(typeof(Country))]
    public class EditCountryInput: EntityDto<int>
    {
    }
}
