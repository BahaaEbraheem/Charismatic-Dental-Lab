using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Specialties.Dto
{
    [AutoMap(typeof(Specialty))]
    public class SpecialitySelecltList
    {
        public int Id { get; set; }

        public int Name { get; set; }
    }
}
