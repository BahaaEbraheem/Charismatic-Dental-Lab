﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Models
{
    public class Specialty : FullAuditedAggregateRoot
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }

    }
}
