using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Utils
{
    internal static class ConditionalAttributeHelper
    {
        internal static bool ShouldValidate(ValidationContext context, string propertyName, object conditionalValue)
        {
            var objectType = context.ObjectInstance.GetType();
            var property = objectType.GetProperty(propertyName);

            if (property == null)
            {
                throw new ValidationException($"Type {objectType} does not have property {propertyName}");
            }

            if (property.PropertyType != conditionalValue.GetType())
            {
                throw new ValidationException(
                    $"Conditional property '{propertyName}' ({property.PropertyType}) is not the same type as provided PropertyValue ({conditionalValue.GetType()})");
            }

            return property.GetValue(context.ObjectInstance).Equals(conditionalValue);
        }
    }
}