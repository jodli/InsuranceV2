using System;

namespace InsuranceV2.Application.Models.Insuree
{
    public class DetailInsuree
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}