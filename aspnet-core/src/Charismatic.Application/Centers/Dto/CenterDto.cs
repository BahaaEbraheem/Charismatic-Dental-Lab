using Abp.Application.Services.Dto;
using Abp.AutoMapper;
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
    public class CenterDto : EntityDto<int>
    {
        [Required]
        [StringLength(600)]
        public string Name { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public int? CountryId { get; set; }
        public string AccountEmail { get; set; }
        public long? CreatorUserId { get; set; }
        public int StateId { get; set; }
        public string CreatorUserName { get; set; }
        public string CityName { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
