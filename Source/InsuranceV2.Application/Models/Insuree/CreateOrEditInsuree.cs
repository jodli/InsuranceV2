using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceV2.Application.Models.Insuree
{
    public class CreateOrEditInsuree : IValidatableObject
    {
        public int Id { get; set; }

        [Required, DisplayName("Vorname")]
        public string FirstName { get; set; }

        [Required, DisplayName("Nachname")]
        public string LastName { get; set; }

        [DisplayName("Geburtsdatum")]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success;
        }
    }
}