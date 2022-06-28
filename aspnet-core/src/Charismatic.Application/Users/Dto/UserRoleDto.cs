using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Users.Dto
{
    [AutoMap(typeof(UserRole))]
    public class UserRoleDto
    {
      
        public long UserId { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public long? CreatorUserId { get; set; }

        public string CreaterUserName { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
