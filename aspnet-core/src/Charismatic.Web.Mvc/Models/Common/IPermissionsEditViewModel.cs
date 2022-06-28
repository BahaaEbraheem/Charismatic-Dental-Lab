using System.Collections.Generic;
using Charismatic.Roles.Dto;

namespace Charismatic.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}