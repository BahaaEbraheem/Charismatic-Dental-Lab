using Abp.AutoMapper;
using Charismatic.Centers.Dto;
using Charismatic.PatientReferrais.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Charismatic.Enums;
namespace Charismatic.Web.Models.Cases
{
    [AutoMapTo(typeof(CreatePatientReferraisDto))]
    public class CreateCenterCaseViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        public int CenterId { get;set; }
        public SelectList Centers { get; set; }
    }
}