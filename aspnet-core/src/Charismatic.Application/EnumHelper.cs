using Abp.Reflection.Extensions;
using Charismatic;
using Charismatic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic
{
    public class EnumHelper : CharismaticAppServiceBase
    {
        public EnumHelper()
        { }


        /// <summary>
        /// convert any enum type to list
        /// </summary>
        public List<EnumObject> GetEnumAsList(string type)
        {
            //convert type from string
            var assembly = typeof(CharismaticCoreModule).GetAssembly();
            var typeAsType = assembly.GetType(type);

            //get list
            List<EnumObject> list = new List<EnumObject>();
            foreach (var obj in Enum.GetValues(typeAsType))
            {
                EnumObject enumObject = new EnumObject();
                Enum enumValue = Enum.Parse(typeAsType, obj.ToString()) as Enum;
                enumObject.Text = EnumHelper<Enum>.GetDisplayValue((Enum)Enum.Parse(typeAsType, Enum.GetName(typeAsType, Convert.ToInt32(enumValue))));
                enumObject.Value = Convert.ToInt32(enumValue);
                list.Add(enumObject);
            }
            return list;
        }
        //public string GetDisplyNameOfUserType(string type)
        //{
        //    //convert type from string
        //    var assembly = typeof(CMMSCoreModule).GetAssembly();
        //    var typeAsType = assembly.GetType(type);

        //    string displayName = "";
        //    foreach (var obj in Enum.GetValues(typeAsType))
        //    {
        //        Enum enumValue = Enum.Parse(typeof(UserTypes), obj.ToString()) as Enum;
        //        if (type == enumValue.ToString())
        //        {
        //            displayName = EnumHelper<Enum>.GetDisplayValue((Enum)Enum.Parse(typeof(UserTypes), Enum.GetName(typeof(UserTypes), Convert.ToInt32(enumValue))));
        //            break;
        //        }
        //    }
        //    return displayName;
        //}

    }
    public class EnumObject
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }



}




