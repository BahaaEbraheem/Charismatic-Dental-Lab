using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models.Address
{
    public class Country:Entity
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(3)]
        public string Iso3 { get; set; }

        [StringLength(3)]
        [Column("numeric_code")]
        public string NumericCode { get; set; }

        [StringLength(2)]
        public string Iso2 { get; set; }

        [StringLength(255)]
        public string PhoneCode { get; set; }

        [StringLength(255)]
        public string Capital { get; set; }

        [StringLength(255)]
        public string Currency { get; set; }

        [StringLength(255)]
        [Column("currency_name")]
        public string CurrencyName { get; set; }

        [StringLength(255)]
        [Column("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [StringLength(255)]
        public string Tld { get; set; }

        [StringLength(255)]
        public string Native { get; set; }

        [StringLength(255)]
        public string Region { get; set; }

        [StringLength(255)]
        public string Subregion { get; set; }

        public string Timezones { get; set; }

        public string Translations { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [StringLength(191)]
        public string  emoji { get; set; }

        [StringLength(191)]
        public string emojiU { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime updatedat { get; set; }

        public int? Flag { get; set; }

        [StringLength(191)]
        public string WikiDataId { get; set; }

        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<PatientReferrais> Patients { get; set; }
    }
}
