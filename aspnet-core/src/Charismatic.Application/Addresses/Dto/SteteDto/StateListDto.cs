using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Addresses.Dto.SteteDto
{

    [AutoMapFrom(typeof(State))]
    public class StateListDto : EntityDto<int>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string FipsCode { get; set; }
        public string Iso2 { get; set; }
        public string Type { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime updatedat { get; set; }
        public int Flag { get; set; }
        public string WikiDataId { get; set; }
        public virtual Country Country { get; set; }
    }
}
