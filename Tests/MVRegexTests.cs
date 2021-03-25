using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelValidation.Validators;
using Tests.Models;

namespace Tests
{
    [TestClass]
    public class MVRegexTests
    {
        [TestMethod]
        public void ValidatePositiveIntegerWithPositiveValue()
        {
            var obj = new ModelWithOneRegexIntField
            {
                RegexField = 20
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ValidatePositiveIntegerWithNegativeValue()
        {
            var obj = new ModelWithOneRegexIntField
            {
                RegexField = -20
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidatePositiveIntegerWithNullValue()
        {
            var obj = new ModelWithOneNullableRegexIntField()
            {
                RegexField = null
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidatePositiveNumberWithNineValues()
        {
            var obj = new ModelWithPositiveNumberWithNoMoreThanNineValues()
            {
                RegexField = 123456789
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ValidatePositiveNumberWithMoreThanNineValues()
        {
            var obj = new ModelWithPositiveNumberWithNoMoreThanNineValues()
            {
                RegexField = int.MaxValue
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateMultipleRegexFields()
        {
            var obj = new ModelWithMultipleValidationAttributes()
            {
                RegexField = 4567,
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);

            obj = new ModelWithMultipleValidationAttributes()
            {
                RegexField = 456,
            };

            validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);

            obj = new ModelWithMultipleValidationAttributes()
            {
                RegexField = -23,
            };

            validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateRegexWithAllowNullSetToTrue()
        {
            var obj = new ModelWithRegExAndAllowNull()
            {
                RegexField = null,
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);

            obj = new ModelWithRegExAndAllowNull()
            {
                RegexField = int.MaxValue,
            };

            validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateRegexWithAllowNullSetToFalse()
        {
            var obj = new ModelWithOneNullableRegexIntField()
            {
                RegexField = null,
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);

            obj = new ModelWithOneNullableRegexIntField()
            {
                RegexField = 2,
            };

            validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);

            obj = new ModelWithOneNullableRegexIntField()
            {
                RegexField = -2
            };

            validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateOnlyNumbersOrNothing()
        {
            var obj = new ModelWithOnlyDigits()
            {
                RegexField = 23,
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);

            obj = new ModelWithOnlyDigits()
            {
                RegexField = null,
            };

            validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }
    }
}
