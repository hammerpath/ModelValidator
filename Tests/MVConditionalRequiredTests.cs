using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelValidation.Validators;
using Tests.Models;

namespace Tests
{
    [TestClass]
    public class MVConditionalRequiredTests
    {
        [TestMethod]
        public void ShouldNotValidateIfConditionNoMet()
        {
            var obj = new ModelWithConditionalRequiredGuid()
            {
                Enabled = false,
                RequiredField = null,
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ShouldNotPassValidationIfConditionIsMetAndValueIsNull()
        {
            var obj = new ModelWithConditionalRequiredGuid()
            {
                Enabled = true,
                RequiredField = null,
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ShouldPassValidationIfConditionIsMetAndValueIsNotNull()
        {
            var obj = new ModelWithConditionalRequiredGuid()
            {
                Enabled = true,
                RequiredField = Guid.Empty,
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ShouldNotPassValidationIfConditionIsMetAndValueIsDefault()
        {
            var obj = new ModelWithConditionalRequiredNotDefaultGuid()
            {
                Enabled = true,
                Value = Guid.Empty
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ShouldPassValidationIfConditionIsMetAndValueIsNotDefault()
        {
            var obj = new ModelWithConditionalRequiredNotDefaultGuid()
            {
                Enabled = true,
                Value = Guid.NewGuid()
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ShouldNotPassValidationIfValueIsEmptyString()
        {
            var obj = new ModelWithConditionalRequiredNotEmptyString
            {
                Enabled = true,
                Value = "       ",
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ShouldPassValidationIfValueIsNotEmptyString()
        {
            var obj = new ModelWithConditionalRequiredNotEmptyString
            {
                Enabled = true,
                Value = "   d    ",
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }
    }
}