using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Addresses.Dto.SteteDto
{
    [AutoMap(typeof(State))]
    class EditStateInput : EntityDto<int>
    {
    }
}
