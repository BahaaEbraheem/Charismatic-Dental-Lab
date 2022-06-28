using System.Collections.Generic;
using Charismatic.Roles.Dto;

namespace Charismatic.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
