using System.Collections.Generic;
using Charismatic.Roles.Dto;

namespace Charismatic.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
