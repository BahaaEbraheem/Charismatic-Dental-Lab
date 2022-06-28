using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Users.Dto
{
    [AutoMapTo(typeof(UserRole))]
    public class CreateUserRoleDto
    {
        public long UserId { get; set; }

        public int RoleId { get; set; }
    }
}
