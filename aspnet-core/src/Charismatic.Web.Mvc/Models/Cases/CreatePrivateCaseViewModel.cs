using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Charismatic.PatientReferrais.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Charismatic.Enums;
namespace Charismatic.Web.Models.Cases
{
    [AutoMap(typeof(CreatePatientReferraisDto))]
    public class CreatePrivateCaseViewModel
    { 
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string StreetAddress { get; set; }

        public string BuildingName { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required]
        public int CountryId { get; set; }

        public int? StateId { get; set; }

        public SelectList Countries { get; set; }
    }
}