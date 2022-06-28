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

namespace Charismatic.Doctors.Dto
{
    public class DoctorMapProfile : Profile
    {
        public DoctorMapProfile()
        {

            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<Doctor, CreateDoctorDto>();

            CreateMap<User, CreateDoctorDto>();
            CreateMap<CreateDoctorDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<DoctorDto, Doctor>();


            CreateMap<Doctor, DoctorDto>();

            CreateMap<Doctor, DoctorDto>()
           .ForMember(x => x.EmailAddress, opt => opt.Ignore())
           .ForMember(x => x.IsActive, opt => opt.Ignore())
           .ForMember(x => x.FullName, opt => opt.Ignore());

            CreateMap<User, DoctorListDto>();
            CreateMap< DoctorListDto, User>();

            CreateMap<EditDoctorDto, UserDto>();
            CreateMap<CreateDoctorDto, DoctorListDto > ();

            CreateMap<DoctorListDto, Doctor>();
            CreateMap<Doctor, DoctorListDto>();
            CreateMap<Doctor, DoctorListDto>()
       .ForMember(x => x.EmailAddress, opt => opt.Ignore())
     .ForMember(x => x.FullName, opt => opt.Ignore())
     .ForMember(x => x.StateName, opt => opt.Ignore());

        //CreateMap<Role, RoleEditDto>();
        //CreateMap<Permission, FlatPermissionDto>();
    }
}
}
