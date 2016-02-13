using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Common.Collections
{
    public class Insurees : CollectionBase<Insuree>
    {
        public Insurees()
        {
        }

        public Insurees(IList<Insuree> initialList) : base(initialList)
        {
        }

        public Insurees(CollectionBase<Insuree> initialList) : base(initialList)
        {
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var insuree in this)
            {
                errors.AddRange(insuree.Validate());
            }
            return errors;
        }
    }
}