using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.DoctorSpecialties.Dto
{
    public class DoctorSpaciltyMapProfile : Profile
    {
        public DoctorSpaciltyMapProfile()
        {
            // Role and permission
            //CreateMap<Permission, string>().ConvertUsing(r => r.Name);
            //CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

            CreateMap<CreateDoctorSpecialtyDto, DoctorSpecialty>();

            CreateMap<DoctorSpecialtyDto, DoctorSpecialty>();

            CreateMap<DoctorSpecialty, DoctorSpecialtyDto>();

            CreateMap<DoctorSpecialty, CreateDoctorSpecialtyDto>();
            //CreateMap<Role, RoleEditDto>();
            //CreateMap<Permission, FlatPermissionDto>();
        }
    }
}
