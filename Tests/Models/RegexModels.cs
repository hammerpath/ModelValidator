using ModelValidation.Attributes;
using ModelValidation.Constants;
using ModelValidation.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tests.Models
{
    public class ModelWithOneNullableRegexIntField : MVModel<ModelWithOneNullableRegexIntField>
    {

        [ModelValidation.Attributes.MVRegularExpressionAttribute(RegexConstants.OnlyPositiveNumbers)]
        public int? RegexField { get; set; }
    }

    public class ModelWithOneRegexIntField : MVModel<ModelWithOneRegexIntField>
    {
        [System.ComponentModel.DataAnnotations.RegularExpression(RegexConstants.OnlyPositiveNumbers)]
        public int RegexField { get; set; }
    }

    public class
        ModelWithPositiveNumberWithNoMoreThanNineValues : MVModel<
            ModelWithPositiveNumberWithNoMoreThanNineValues>
    {
        [System.ComponentModel.DataAnnotations.RegularExpression(RegexConstants.OnlyPositiveNumbersWithNoMoreThanNineValues)]
        public int RegexField { get; set; }
    }

    public class ModelWithMultipleValidationAttributes : MVModel<ModelWithMultipleValidationAttributes>
    {
        [ModelValidation.Attributes.MVRegularExpressionAttribute(RegexConstants.MaxThreeChars)]
        [ModelValidation.Attributes.MVRegularExpressionAttribute(RegexConstants.OnlyPositiveNumbersWithNoMoreThanNineValues)]
        public int RegexField { get; set; }
    }

    public class ModelWithRegExAndAllowNull : MVModel<ModelWithMultipleValidationAttributes>
    {
        [ModelValidation.Attributes.MVRegularExpressionAttribute(RegexConstants.OnlyPositiveNumbersWithNoMoreThanNineValues, AllowNull = true)]
        public int? RegexField { get; set; }
    }

    public class ModelWithOnlyDigits : MVModel<ModelWithMultipleValidationAttributes>
    {
        [ModelValidation.Attributes.MVRegularExpressionAttribute(RegexConstants.OnlyNumbers)]
        public int? RegexField { get; set; }
    }

    public class RegexFieldModel
    {
        public IEnumerable<BaseValidationModel> RegExField { get; set; }
    }
}
