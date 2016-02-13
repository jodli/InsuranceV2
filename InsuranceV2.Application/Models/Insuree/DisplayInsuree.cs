using System;
using System.ComponentModel;

namespace InsuranceV2.Application.Models.Insuree
{
    public class DisplayInsuree
    {
        public int Id { get; set; }

        [DisplayName("Vorname")]
        public string FirstName { get; set; }

        [DisplayName("Nachname")]
        public string LastName { get; set; }

        public string FullName { get; set; }
    }
}