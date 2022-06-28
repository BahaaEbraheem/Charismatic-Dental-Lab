using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using AutoMapper;
using Charismatic.Authorization.Users;
using Charismatic.Models;
using Charismatic.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Departments.Dto
{
    public class DepartmentMapProfile : Profile
    {
        public DepartmentMapProfile()
        {

            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<Department, CreateDepartmentDto>();

    }
}
}
