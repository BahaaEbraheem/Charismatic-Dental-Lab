using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Authorization
{
    public class CharismaticAuthorizeAttribute : AbpAuthorizeAttribute
    {
        // <summary>
        /// If this property is set to false, only one attribute is enough to access the method. Default true
        /// </summary>
        public bool RequireAllAttributes { get; set; } = true;
        public long? AccessUserId { get; set; }
        public CharismaticAuthorizeAttribute(params string[] permissions) : base(permissions)
        {

        }

        public CharismaticAuthorizeAttribute(string moduleName) : base(GetPermissionsNamesMatchingModule(moduleName))
        {
        }

        private static string[] GetPermissionsNamesMatchingModule(string module)
        {
            List<string> constants = new List<string>();

            foreach (var constant in typeof(PermissionNames).GetFields())
            {
                if (constant.IsLiteral && !constant.IsInitOnly)
                {
                    string constantName = (string)constant.GetValue(null);
                    if (constantName.StartsWith(module))
                        constants.Add(constantName);
                }
            }
            return constants.ToArray();
        }
    }
}
