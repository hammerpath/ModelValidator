using ModelValidation.Attributes;
using ModelValidation.Constants;
using ModelValidation.Models;
using System.Collections.Generic;
using Tests.Enums;

namespace Tests.Models
{
    public class ModelWithPositiveNumberAndDependentBoolProperty : MVModel<ModelWithOneNullableRegexIntField>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, nameof(Enabled), true)]
        public int? RegexField { get; set; }
        public bool Enabled { get; set; }
    }

    public class ModelWithInvalidDependentProperty : MVModel<ModelWithOneNullableRegexIntField>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, "Nope", true)]
        public int? RegexField { get; set; }
    }

    public class ModelWithPositiveNumberAndDependentStringProperty : MVModel<ModelWithOneNullableRegexIntField>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, nameof(Enabled), "true")]
        public int? RegexField { get; set; }
        public string Enabled { get; set; }
    }

    public class ModelWithIncorrectPropertyValue : MVModel<ModelWithOneNullableRegexIntField>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, nameof(Enabled), "test")]
        public int? RegexField { get; set; }
        public bool Enabled { get; set; }
    }

    public class ModelWithIncorrectDependentPropertyType : MVModel<ModelWithOneNullableRegexIntField>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, nameof(Enabled), true)]
        public int? RegexField { get; set; }
        public int Enabled { get; set; }
    }

    public class ModelWithIncorrectEnumDependentPropertyType : MVModel<ModelWithOneNullableRegexIntField>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, nameof(TestEnum), DummyEnum.One)]
        public int? RegexField { get; set; }
        public TestEnum TestEnum { get; set; }
    }

    public class ConditionRegexModel : MVModel<ConditionRegexModel>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, nameof(Enabled), true)]
        public IEnumerable<BaseValidationModel> RegexField { get; set; }

        public bool Enabled { get; } = true;
    }

    public class ModelWithConditionalEnumValue : MVModel<ModelWithConditionalEnumValue>
    {
        [MVConditionRegularExpression(RegexConstants.OnlyPositiveNumbers, nameof(TestEnum), TestEnum.One)]
        public int? RegexField { get; set; }
        public TestEnum TestEnum { get; set; }

    }
}
