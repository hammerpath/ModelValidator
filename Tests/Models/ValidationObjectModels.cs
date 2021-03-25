using ModelValidation.Attributes;
using ModelValidation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tests.Models
{
    public static class ErrorMessageConstants
    {
        public const string Required = "This is required!";
    }

    public class ModelWithNoAttributes : MVModel<ModelWithNoAttributes>
    {
    }

    public class ModelWithErrorMessage : MVModel<ModelWithErrorMessage>
    {
        [Required(ErrorMessage = ErrorMessageConstants.Required)]
        public IEnumerable<BaseValidationModel> RequiredField { get; set; }
    }

    public class ModelWithInheritedAttributes : ModelBaseWithRequiredAttribute<ModelWithInheritedAttributes>
    {

    }

    public abstract class ModelBaseWithRequiredAttribute<T> : MVModel<T> where T : class
    {
        [MVRequired]
        public Guid? Guid { get; set; }
    }

    public class BaseValidationModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ErrorMessage { get; set; }
        public string ConditionPropertyName { get; set; }
        public object ConditionValue { get; set; }
    }
}
