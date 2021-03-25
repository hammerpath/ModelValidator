using ModelValidation.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ModelValidation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MVConditionalRequiredAttribute : MVRequiredAttribute
    {
        [NotNull] public readonly string PropertyName;
        public readonly object PropertyValue;

        public MVConditionalRequiredAttribute([NotNull] string propertyName, object propertyValue)
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
    }
}