using Abp.AutoMapper;
using Charismatic.Centers.Dto;
using Charismatic.Localization.SourceFiles;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic.Web.Models.Centers
{
    [AutoMapTo(typeof(CreateCenterDto))]
    public class CreateCenterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = nameof(DataAnnotations.Required))]
        [StringLength(600, ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = nameof(DataAnnotations.StringLength))]
        public string Name { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public int? CountryId { get; set; }
        public int StateId { get; set; }
        public string AccountEmail { get; set; }
        public SelectList Countries { get; set; }
    }
}
