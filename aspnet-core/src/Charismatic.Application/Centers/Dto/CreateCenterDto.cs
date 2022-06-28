using Abp.AutoMapper;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Centers.Dto
{
    [AutoMap(typeof(Center))]
    public class CreateCenterDto 
    {
        [Required(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = nameof(DataAnnotations.Required))]
        [StringLength(600, ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = nameof(DataAnnotations.StringLength))]
        public string Name { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string AccountEmail { get; set; }

    }
}
