using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InsuranceV2.Common.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException()
        {
        }

        public ModelValidationException(string message) : base(message)
        {
        }

        public ModelValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ModelValidationException(string message, Exception innerException,
            IEnumerable<ValidationResult> validationResults) : base(message, innerException)
        {
            ValidationResults = validationResults;
        }

        public ModelValidationException(string message, IEnumerable<ValidationResult> validationResults) : base(message)
        {
            ValidationResults = validationResults;
        }

        protected ModelValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IEnumerable<ValidationResult> ValidationResults { get; private set; }
    }
}