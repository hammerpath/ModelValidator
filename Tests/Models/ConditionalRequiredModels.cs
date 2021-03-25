using ModelValidation.Attributes;
using ModelValidation.Models;
using System;

namespace Tests.Models
{
    public class ModelWithConditionalRequiredNotEmptyString : MVModel<ModelWithConditionalRequiredNotEmptyString>
    {
        public bool Enabled { get; set; }
        [MVConditionalRequired(nameof(Enabled), true, AllowEmptyStrings = false)]
        public string Value { get; set; }
    }

    public class ModelWithConditionalRequiredGuid : MVModel<ModelWithConditionalRequiredGuid>
    {
        public bool Enabled { get; set; }
        [MVConditionalRequired(nameof(Enabled), true)]
        public Guid? RequiredField { get; set; }
    }

    public class ModelWithConditionalRequiredNotDefaultGuid : MVModel<ModelWithConditionalRequiredNotDefaultGuid>
    {
        public bool Enabled { get; set; }
        [MVConditionalRequired(nameof(Enabled), true, AllowDefaultValue = false)]
        public Guid Value { get; set; }
    }
}
