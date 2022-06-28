using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Doctors.Dto
{
    [AutoMapFrom(typeof(Doctor))]

   public  class EditDoctorDto : CreateDoctorDto, IEntityDto<int>
    {

        public int Id { get; set; }
    }
}
