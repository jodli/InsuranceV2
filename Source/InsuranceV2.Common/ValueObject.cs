using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace InsuranceV2.Common
{
    public abstract class ValueObject<T> : IEquatable<T>, IValidatableObject where T : ValueObject<T>
    {
        public bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var publicPropertyInfos = GetType().GetProperties();

            if (publicPropertyInfos.Any())
            {
                return publicPropertyInfos.All(p => CheckValue(p, other));
            }
            return true;
        }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        public IEnumerable<ValidationResult> Validate()
        {
            var validateErrors = new List<ValidationResult>();
            var ctx = new ValidationContext(this);
            Validator.TryValidateObject(this, ctx, validateErrors, true);
            return validateErrors;
        }

        private bool CheckValue(PropertyInfo p, T other)
        {
            var left = p.GetValue(this);
            var right = p.GetValue(other);

            if (left == null || right == null)
            {
                return false;
            }

            if (left is T)
            {
                return ReferenceEquals(left, right);
            }
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var item = obj as ValueObject<T>;

            if (item != null)
            {
                return Equals((T) item);
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 31;
            var changeMultiplier = false;
            var index = 1;

            var publicProperties = GetType().GetProperties();

            if (publicProperties.Any())
            {
                foreach (var item in publicProperties)
                {
                    var value = item.GetValue(this, null);

                    if (value != null)
                    {
                        hashCode = hashCode*(changeMultiplier ? 59 : 114) + value.GetHashCode();
                        changeMultiplier = !changeMultiplier;
                    }
                    else
                    {
                        hashCode = hashCode ^ (index*13);
                    }
                }
            }
            return hashCode;
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }
    }
}