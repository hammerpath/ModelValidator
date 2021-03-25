using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelValidation.Validators;
using System.ComponentModel.DataAnnotations;
using Tests.Enums;
using Tests.Models;

namespace Tests
{
    [TestClass]
    public class MVConditionRegexTests
    {
        [TestMethod]
        public void ValidateInvalidRegexWithDependentStringProperty()
        {
            var notEnabled = new ModelWithPositiveNumberAndDependentStringProperty()
            {
                RegexField = -40,
                Enabled = "false"
            };

            var enabled = new ModelWithPositiveNumberAndDependentStringProperty()
            {
                RegexField = -40,
                Enabled = "true"
            };

            var validationNotEnabled = ModelValidator.ValidateEntity(notEnabled);
            var validationEnabled = ModelValidator.ValidateEntity(enabled);

            Assert.IsFalse(validationNotEnabled.HasError);
            Assert.IsTrue(validationEnabled.HasError);
        }

        [TestMethod]
        public void ValidateValidRegexWithDependentStringProperty()
        {
            var notEnabled = new ModelWithPositiveNumberAndDependentStringProperty()
            {
                RegexField = 40,
                Enabled = "false"
            };

            var enabled = new ModelWithPositiveNumberAndDependentStringProperty()
            {
                RegexField = 40,
                Enabled = "true"
            };

            var validationNotEnabled = ModelValidator.ValidateEntity(notEnabled);
            var validationEnabled = ModelValidator.ValidateEntity(enabled);

            Assert.IsFalse(validationNotEnabled.HasError);
            Assert.IsFalse(validationEnabled.HasError);
        }

        [TestMethod]
        public void ValidateInvalidRegexWithDependentBoolProperty()
        {
            var notEnabled = new ModelWithPositiveNumberAndDependentBoolProperty()
            {
                RegexField = -40,
                Enabled = false
            };

            var enabled = new ModelWithPositiveNumberAndDependentBoolProperty()
            {
                RegexField = -40,
                Enabled = true
            };

            var validationNotEnabled = ModelValidator.ValidateEntity(notEnabled);
            var validationEnabled = ModelValidator.ValidateEntity(enabled);

            Assert.IsFalse(validationNotEnabled.HasError);
            Assert.IsTrue(validationEnabled.HasError);
        }

        [TestMethod]
        public void ValidateValidRegexWithDependentBoolProperty()
        {
            var notEnabled = new ModelWithPositiveNumberAndDependentBoolProperty()
            {
                RegexField = 40,
                Enabled = false
            };

            var enabled = new ModelWithPositiveNumberAndDependentBoolProperty()
            {
                RegexField = 40,
                Enabled = true
            };

            var validationNotEnabled = ModelValidator.ValidateEntity(notEnabled);
            var validationEnabled = ModelValidator.ValidateEntity(enabled);

            Assert.IsFalse(validationNotEnabled.HasError);
            Assert.IsFalse(validationEnabled.HasError);
        }

        [TestMethod]
        public void ShouldThrowBecauseOfNonExistingPropertyName()
        {
            var invalid = new ModelWithInvalidDependentProperty();

            Assert.ThrowsException<ValidationException>(() => ModelValidator.ValidateEntity(invalid));
        }

        [TestMethod]
        public void ShouldThrowBecauseOfNonExistingPropertyValue()
        {
            var invalid = new ModelWithIncorrectPropertyValue
            {
                Enabled = true,
                RegexField = 20
            };

            Assert.ThrowsException<ValidationException>(() => ModelValidator.ValidateEntity(invalid));
        }

        [TestMethod]
        public void ShouldThrowBecauseOfPropertyValueNotBeingABool()
        {
            var invalid = new ModelWithIncorrectDependentPropertyType
            {
                Enabled = 2,
                RegexField = 20
            };

            Assert.ThrowsException<ValidationException>(() => ModelValidator.ValidateEntity(invalid));
        }

        [TestMethod]
        public void ShouldThrowBecauseOfPropertyValueNotBeingSameTypeAsValue()
        {
            var conditional = new ModelWithIncorrectEnumDependentPropertyType()
            {
                TestEnum = TestEnum.One,
                RegexField = 2
            };

            Assert.ThrowsException<ValidationException>(() => ModelValidator.ValidateEntity(conditional));
        }

        [TestMethod]
        public void ShouldPassValidationIfConditionalEnumConditionsIsNotMet()
        {
            var conditional = new ModelWithConditionalEnumValue
            {
                TestEnum = TestEnum.Two,
                RegexField = -1
            };

            var result = ModelValidator.ValidateEntity(conditional);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public void ShouldNotPassValidationIfConditionalEnumConditionsIsMetAndValueIsInvalid()
        {
            var conditional = new ModelWithConditionalEnumValue
            {
                TestEnum = TestEnum.One,
                RegexField = -1
            };

            var result = ModelValidator.ValidateEntity(conditional);
            Assert.IsTrue(result.HasError);
        }

        [TestMethod]
        public void ShouldPassValidationIfConditionalEnumConditionsIsMetAndValueIsCorrect()
        {
            var conditional = new ModelWithConditionalEnumValue
            {
                TestEnum = TestEnum.One,
                RegexField = 2
            };

            var result = ModelValidator.ValidateEntity(conditional);
            Assert.IsFalse(result.HasError);
        }
    }
}
