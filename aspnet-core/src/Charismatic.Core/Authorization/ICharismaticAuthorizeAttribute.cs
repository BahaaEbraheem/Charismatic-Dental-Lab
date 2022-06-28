using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Authorization
{
    public interface ICharismaticAuthorizeAttribute : IAbpAuthorizeAttribute
    {
        bool RequireAllAttributes { get; set; }
    }

}
