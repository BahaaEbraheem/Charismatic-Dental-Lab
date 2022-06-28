using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Addresses.Dto.CountryDto
{
    [AutoMapFrom(typeof(Country))]
    public class CountryListDto: EntityDto<int>
    {
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public string Iso2 { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string Tld { get; set; }
        public string Native { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public string Timezones { get; set; }
        public string Translations { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string emoji { get; set; }
        public string emojiU { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime updatedat { get; set; }
        public int Flag { get; set; }
        public string WikiDataId { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
