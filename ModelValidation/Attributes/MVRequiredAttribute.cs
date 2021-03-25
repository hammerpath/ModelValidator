using ModelValidation.Utils;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MVRequiredAttribute : RequiredAttribute
    {
        public bool AllowEmptyCollection { get; set; } = true;
        public bool AllowDefaultValue { get; set; } = true;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!AllowEmptyCollection)
            {
                switch (value)
                {
                    case ICollection collection when collection.Count == 0:
                        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                    case IEnumerable enumerable when !enumerable.GetEnumerator().MoveNext():
                        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                }
            }

            if (AllowDefaultValue) return base.IsValid(value, validationContext);

            var defaultValue = value.GetType().GetDefaultValue();

            return Equals(value, defaultValue)
                ? new ValidationResult(ErrorMessage, new[] { validationContext.MemberName })
                : base.IsValid(value, validationContext);
        }

        public string GetErrorMessage()
        {
            return ErrorMessageString;
        }
    }
}
