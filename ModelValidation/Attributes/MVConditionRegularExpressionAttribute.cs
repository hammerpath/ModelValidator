using ModelValidation.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ModelValidation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MVConditionRegularExpressionAttribute : MVRegularExpressionBaseAttribute
    {
        [NotNull] public readonly string PropertyName;
        public readonly object PropertyValue;

        public MVConditionRegularExpressionAttribute([NotNull] string pattern, [NotNull] string propertyName, object propertyValue) : base(pattern)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ConditionalAttributeHelper.ShouldValidate(validationContext, PropertyName, PropertyValue)
                ? base.IsValid(value, validationContext)
                : ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return ErrorMessageString;
        }
    }
}