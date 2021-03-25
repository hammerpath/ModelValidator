using ModelValidation.Constants;
using ModelValidation.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tests.Models
{
    public class ModelWithRequiredRegexIntField : MVModel<ModelWithRequiredRegexIntField>
    {
        [Required]
        [RegularExpression(RegexConstants.OnlyPositiveNumbers)]
        public int RequiredRegexField { get; set; }
    }

    public class RequiredRegexFieldModel
    {
        public IEnumerable<BaseValidationModel> RequiredRegexField { get; set; }
    }

    public class ModelWithRequiredAndRegexIntField : MVModel<ModelWithRequiredAndRegexIntField>
    {

        [RegularExpression(RegexConstants.OnlyPositiveNumbers)]
        public int RegexField { get; set; }

        [Required]
        public bool RequiredField { get; set; }
    }

    public class RequiredAndRegexFieldModel
    {
        public IEnumerable<BaseValidationModel> RequiredField { get; set; }
        public IEnumerable<BaseValidationModel> RegexField { get; set; }
    }
}
