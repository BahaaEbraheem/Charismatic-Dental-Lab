using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Cases.Dto
{
    [AutoMapTo(typeof(Case))]
    public class UpdateCaseInput: EntityDto<int>
    {

    }
}
