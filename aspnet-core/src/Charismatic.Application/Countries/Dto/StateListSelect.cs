using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Charismatic.Models.Address;

namespace Charismatic.Countries.Dto
{
    [AutoMap(typeof(State))]
    public class StateListSelect
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
