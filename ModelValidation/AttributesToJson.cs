using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ModelValidation.Attributes;
using ModelValidation.Utils;

namespace ModelValidation
{
    internal class ValidationProperty
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ErrorMessage { get; set; }
        public string ConditionPropertyName { get; set; }
        public object ConditionValue { get; set; }
        public bool AllowNull { get; set; }
        public bool AllowEmptyString { get; set; } = true;
        public bool AllowEmptyCollection { get; set; } = true;
    }

    public static class JsonAttributeConstant
    {
        public const string JsonRequiredAttribute = "required";
        public const string JsonRegularExpressionAttribute = "regex";
        public const string JsonConditionRegularExpressionAttribute = "regex-condition";
        public const string JsonConditionalRequiredAttribute = "required-condition";
    }

    /// <summary>
    /// Creates json objects from c# attributes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AttributesToJson<T> where T : class
    {
        /// <summary>
        /// Converts attributes from the current model to a json validation object
        /// </summary>
        /// <returns></returns>
        internal static JObject GetValidationObjectFromAttributes()
        {
            var validationObject = new ExpandoObject() as IDictionary<string, object>;

            //get object properties which has attributes
            var props = new List<PropertyInfo>(typeof(T).GetProperties().Where(a => a.CustomAttributes.Any()));

            foreach (var prop in props)
            {
                var validationProperties = new List<ValidationProperty>();

                //get property attributes
                var propValues = prop.GetCustomAttributes().ToList();

                foreach (var attribute in propValues)
                {

                    switch (attribute)
                    {
                        case MVConditionalRequiredAttribute conditionalRequired:
                            validationProperties.Add(new ValidationProperty()
                            {
                                Key = JsonAttributeConstant.JsonConditionalRequiredAttribute,
                                Value = "true",
                                ErrorMessage = conditionalRequired.GetErrorMessage(),
                                AllowEmptyString = conditionalRequired.AllowEmptyStrings,
                                AllowEmptyCollection = conditionalRequired.AllowEmptyCollection,
                                ConditionPropertyName = conditionalRequired.PropertyName,
                                ConditionValue = conditionalRequired.PropertyValue,
                            });
                            break;
                        case MVRequiredAttribute jsonRequiredAttribute:
                            validationProperties.Add(new ValidationProperty()
                            {
                                Key = JsonAttributeConstant.JsonRequiredAttribute,
                                Value = "true",
                                ErrorMessage = jsonRequiredAttribute.GetErrorMessage(),
                                AllowEmptyString = jsonRequiredAttribute.AllowEmptyStrings,
                                AllowEmptyCollection = jsonRequiredAttribute.AllowEmptyCollection,
                            });
                            break;
                        case MVConditionRegularExpressionAttribute conditionRegEx:
                            validationProperties.Add(new ValidationProperty()
                            {
                                Key = JsonAttributeConstant.JsonConditionRegularExpressionAttribute,
                                Value = conditionRegEx.Pattern.EscapeBackSlash(),
                                ErrorMessage = conditionRegEx.GetErrorMessage(),
                                ConditionPropertyName = conditionRegEx.PropertyName,
                                ConditionValue = conditionRegEx.PropertyValue,
                                AllowNull = conditionRegEx.AllowNull,
                                AllowEmptyString = conditionRegEx.AllowEmptyString,
                            });
                            break;
                        case Attributes.MVRegularExpressionAttribute jsonRegexAttribute:
                            validationProperties.Add(new ValidationProperty()
                            {
                                Key = JsonAttributeConstant.JsonRegularExpressionAttribute,
                                Value = jsonRegexAttribute.Pattern.EscapeBackSlash(),
                                ErrorMessage = jsonRegexAttribute.GetErrorMessage(),
                                AllowNull = jsonRegexAttribute.AllowNull,
                                AllowEmptyString = jsonRegexAttribute.AllowEmptyString,
                            });
                            break;
                        case RequiredAttribute requiredAttribute:
                            validationProperties.Add(new ValidationProperty()
                            {
                                Key = JsonAttributeConstant.JsonRequiredAttribute,
                                Value = "true",
                                ErrorMessage = requiredAttribute.ErrorMessage,
                                AllowEmptyString = requiredAttribute.AllowEmptyStrings,
                            });
                            break;
                        case System.ComponentModel.DataAnnotations.RegularExpressionAttribute regexAttribute:
                            validationProperties.Add(new ValidationProperty()
                            {
                                Key = JsonAttributeConstant.JsonRegularExpressionAttribute,
                                Value = regexAttribute.Pattern.EscapeBackSlash(),
                                ErrorMessage = regexAttribute.ErrorMessage
                            });
                            break;
                        default:
                            throw new NotImplementedException(attribute.GetType().FullName);
                    }
                }

                validationObject.Add(prop.Name, validationProperties);
            }

            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            serializer.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));

            var item = JObject.FromObject(validationObject, serializer);

            return item;
        }
    }
}
