using System;
using System.Collections.Generic;
using InsuranceV2.Application.Models.Address;

namespace InsuranceV2.Application.Models.Insuree
{
    public class DetailInsuree
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public IEnumerable<DetailAddress> Addresses { get; set; }
    }
}