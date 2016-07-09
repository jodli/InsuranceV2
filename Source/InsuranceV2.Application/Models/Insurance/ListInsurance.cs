using System;

namespace InsuranceV2.Application.Models.Insurance
{
    public class ListInsurance
    {
        public int Id { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime StartDate { get; set; }
    }
}