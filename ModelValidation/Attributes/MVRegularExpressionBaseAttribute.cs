using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ModelValidation.Attributes
{
    public abstract class MVRegularExpressionBaseAttribute : RegularExpressionAttribute
    {
        public bool AllowNull { get; set; } = false;
        public bool AllowEmptyString { get; set; } = false;

        protected MVRegularExpressionBaseAttribute([NotNull] string pattern) : base(pattern)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && !AllowNull || !AllowEmptyString && value != null && value.Equals(string.Empty))
            {
                return new ValidationResult(ErrorMessageString, new[] { validationContext.MemberName });
            }

            return base.IsValid(value, validationContext);
        }
    }
}