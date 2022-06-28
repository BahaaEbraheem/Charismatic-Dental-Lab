using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>
    {
      
        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreationTime { get; set; }


    }
}
