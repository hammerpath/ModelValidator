using ModelValidation.Attributes;
using ModelValidation.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tests.Models
{
    public class ModelWithOneRequiredStringField : MVModel<ModelWithOneRequiredStringField>
    {
        [Required]
        public string RequiredField { get; set; }
    }

    public class RequiredFieldModel
    {
        public IEnumerable<BaseValidationModel> RequiredField { get; set; }
    }

    public class ModelWithRequiredDefaultValueNotAllowed : MVModel<ModelWithRequiredDefaultValueNotAllowed>
    {
        [MVRequired(AllowDefaultValue = false)]
        public int Value { get; set; }
    }

    public class ModelWithRequiredDefaultValueAllowed : MVModel<ModelWithRequiredDefaultValueAllowed>
    {
        [MVRequired]
        public int Value { get; set; }
    }

    public class ModelWithRequiredNonEmptyCollection : MVModel<ModelWithRequiredNonEmptyCollection>
    {
        public ModelWithRequiredNonEmptyCollection()
        {
            Array = Enumerable.Range(1, 10).Select(x => $"{x}").ToArray();
            List = Array.ToList();
            Collection = Array.ToHashSet();
            IEnumerable = Array.ToDictionary(x => x);
        }

        [MVRequired(AllowEmptyCollection = false)]
        public List<object> List { get; set; }

        [MVRequired(AllowEmptyCollection = false)]
        public object[] Array { get; set; }

        [MVRequired(AllowEmptyCollection = false)]
        public ICollection<object> Collection { get; set; }

        [MVRequired(AllowEmptyCollection = false)]
        public IEnumerable IEnumerable { get; set; }
    }

    public class ModelWithRequiredCollection : MVModel<ModelWithRequiredNonEmptyCollection>
    {
        [MVRequired]
        public List<object> List { get; set; } = new List<object>();

        [MVRequired]
        public object[] Array { get; set; } = new object[0];

        [MVRequired]
        public ICollection<object> Collection { get; set; } = new HashSet<object>();

        [MVRequired]
        public IEnumerable IEnumerable { get; set; } = new Dictionary<string, object>();
    }
}
