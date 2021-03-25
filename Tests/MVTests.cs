using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelValidation;
using ModelValidation.Constants;
using ModelValidation.Validators;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Tests.Models;

namespace Tests
{
    [TestClass]
    public class MVTests
    {
        [TestMethod]
        public void ContainsValidationObject()
        {
            var obj = new ModelWithOneRequiredStringField();

            var result = obj.Validation;

            Assert.IsTrue(result.ToString().Contains("requiredField"));
        }

        [TestMethod]
        public void PossibleToConvertWithNoAttributes()
        {
            var obj = new ModelWithNoAttributes();

            var result = obj.Validation;

            Assert.IsTrue(result != null);

        }

        [TestMethod]
        public void AddsRequiredBoolToRequiredField()
        {
            var obj = new ModelWithOneRequiredStringField();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<RequiredFieldModel>(result.ToString());

            Assert.IsTrue(deserializedItem.RequiredField != null);
            Assert.IsTrue(bool.Parse(deserializedItem.RequiredField.First(a => a.Key == JsonAttributeConstant.JsonRequiredAttribute).Value));

        }

        [TestMethod]
        public void AddsRegexPatternToRegExField()
        {
            var obj = new ModelWithOneRegexIntField();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<RegexFieldModel>(result.ToString());

            Assert.IsTrue(deserializedItem != null);
            Assert.IsTrue(deserializedItem.RegExField.First(a => a.Key == JsonAttributeConstant.JsonRegularExpressionAttribute).Value == RegexConstants.OnlyPositiveNumbers);

        }

        [TestMethod]
        public void AddsRequiredRegexPattern()
        {
            var obj = new ModelWithRequiredRegexIntField();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<RequiredRegexFieldModel>(result.ToString());

            Assert.IsTrue(deserializedItem != null);
            Assert.IsTrue(bool.Parse(deserializedItem.RequiredRegexField.First(a => a.Key == JsonAttributeConstant.JsonRequiredAttribute).Value));
            Assert.IsTrue(deserializedItem.RequiredRegexField.First(a => a.Key == JsonAttributeConstant.JsonRegularExpressionAttribute).Value == RegexConstants.OnlyPositiveNumbers);

        }

        [TestMethod]
        public void AddsRequiredAndRegex()
        {
            var obj = new ModelWithRequiredAndRegexIntField();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<RequiredAndRegexFieldModel>(result.ToString());

            Assert.IsTrue(deserializedItem != null);
            Assert.IsTrue(bool.Parse(deserializedItem.RequiredField.First(a => a.Key == JsonAttributeConstant.JsonRequiredAttribute).Value));
            Assert.IsTrue(deserializedItem.RegexField.First(a => a.Key == JsonAttributeConstant.JsonRegularExpressionAttribute).Value == RegexConstants.OnlyPositiveNumbers);

        }

        [TestMethod]
        public void AddsRequiredWithErrorMessage()
        {
            var obj = new ModelWithErrorMessage();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<ModelWithErrorMessage>(result.ToString());

            Assert.IsTrue(deserializedItem != null);
            Assert.IsTrue(bool.Parse(deserializedItem.RequiredField.First(a => a.Key == JsonAttributeConstant.JsonRequiredAttribute).Value));
            Assert.IsTrue(deserializedItem.RequiredField.First(a => a.Key == JsonAttributeConstant.JsonRequiredAttribute).ErrorMessage == ErrorMessageConstants.Required);

        }

        [TestMethod]
        public void AddsRegexPatternForConditionRegex()
        {
            var obj = new ConditionRegexModel();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<ConditionRegexModel>(result.ToString());

            Assert.IsTrue(deserializedItem != null);
            Assert.IsTrue(deserializedItem.RegexField.First(a => a.Key == JsonAttributeConstant.JsonConditionRegularExpressionAttribute).Value == RegexConstants.OnlyPositiveNumbers);
            Assert.IsTrue(deserializedItem.RegexField.First().ConditionPropertyName == nameof(obj.Enabled));
            Assert.AreEqual(deserializedItem.RegexField.First().ConditionValue, true);
        }

        [TestMethod]
        public void HandlesInheritedAttributes()
        {
            var obj = new ModelWithInheritedAttributes();
            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsTrue(validation.HasError);

            obj.Guid = Guid.NewGuid();
            validation = ModelValidator.ValidateEntity(obj);

            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void AddsConditionalEnumAsStringValue()
        {
            var obj = new ModelWithConditionalEnumValue();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<ConditionRegexModel>(result.ToString(), new StringEnumConverter(new CamelCaseNamingStrategy()));

            Assert.IsTrue(deserializedItem.RegexField != null);
            Assert.AreEqual(deserializedItem.RegexField.First().ConditionValue, "one");
        }

        [TestMethod]
        public void AddsConditionToConditionalRequiredAttribute()
        {
            var obj = new ModelWithConditionalRequiredGuid();

            var result = obj.Validation;
            var deserializedItem = JsonConvert.DeserializeObject<ModelWithErrorMessage>(result.ToString());

            Assert.IsTrue(deserializedItem != null);
            Assert.AreEqual(1, deserializedItem.RequiredField.Count());
            var validation = deserializedItem.RequiredField.First();

            Assert.AreEqual(nameof(ModelWithConditionalRequiredGuid.Enabled), validation.ConditionPropertyName);
            Assert.IsTrue(validation.ConditionValue.Equals(true));
            Assert.AreEqual(JsonAttributeConstant.JsonConditionalRequiredAttribute, validation.Key);
        }
    }
}
