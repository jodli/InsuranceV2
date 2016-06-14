using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceV2.Common
{
    public abstract class DomainEntity<T> : IValidatableObject
    {
        public T Id { get; set; }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DomainEntity<T>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var item = (DomainEntity<T>) obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            return item.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                return Id.GetHashCode() ^ 31;
            }
            return base.GetHashCode();
        }

        public static bool operator ==(DomainEntity<T> left, DomainEntity<T> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }
            return left.Equals(right);
        }

        public static bool operator !=(DomainEntity<T> left, DomainEntity<T> right)
        {
            return !(left == right);
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var validationErrors = new List<ValidationResult>();
            var ctx = new ValidationContext(this);
            Validator.TryValidateObject(this, ctx, validationErrors, true);
            return validationErrors;
        }
    }
}