using System;

namespace InsuranceV2.Application.Models.Insurance
{
    public class DetailInsurance
    {
        public int Id { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime StartDate { get; set; }
    }
}