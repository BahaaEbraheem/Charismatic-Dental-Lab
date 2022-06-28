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
    public class State:Entity
    {
        [StringLength(255)]
        public string Name { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("country_code")]
        [StringLength(2)]
        public string CountryCode { get; set; }

        [Column("fips_code")]
        [StringLength(255)]
        public string FipsCode { get; set; }

        [StringLength(255)]
        public string Iso2 { get; set; }


        [StringLength(191)]
        public string Type { get; set; }


        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime updatedat { get; set; }

        public int Flag { get; set; }

        [StringLength(191)]
        public string WikiDataId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<PatientReferrais> Patients { get; set; }
    }
}
