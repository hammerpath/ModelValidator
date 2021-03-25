using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelValidation.Validators;
using Tests.Models;

namespace Tests
{
    [TestClass]
    public class MVRequiredTests
    {
        [TestMethod]
        public void ValidatePopulatedRequiredField()
        {
            var obj = new ModelWithOneRequiredStringField
            {
                RequiredField = "required"
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ValidateEmptyStringRequiredField()
        {
            var obj = new ModelWithOneRequiredStringField
            {
                RequiredField = string.Empty
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateNullRequiredField()
        {
            var obj = new ModelWithOneRequiredStringField
            {
                RequiredField = null
            };

            var validation = ModelValidator.ValidateEntity(obj);

            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateDefaultValueRequiredField_ShouldNotPassIfDefaultValueIsProvided()
        {
            var obj = new ModelWithRequiredDefaultValueNotAllowed()
            {
                Value = 0
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateDefaultValueRequiredField_ShouldPassIfNonDefaultValueIsProvided()
        {
            var obj = new ModelWithRequiredDefaultValueNotAllowed()
            {
                Value = 1
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ValidateDefaultValueNotRequiredField_ShouldPassIfDefaultValueIsProvided()
        {
            var obj = new ModelWithRequiredDefaultValueAllowed()
            {
                Value = 0
            };

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ValidateEmptyCollectionNotAllowedField_ShouldNotPassIfEmptyCollectIsProvided()
        {
            var obj = new ModelWithRequiredNonEmptyCollection();
            obj.Array = Enumerable.Empty<string>().ToArray();
            obj.List = obj.Array.ToList();
            obj.Collection = obj.Array.ToHashSet();
            obj.IEnumerable = obj.Array.ToDictionary(x => x);

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
            Assert.AreEqual(4, validation.Errors.Count);
        }

        [TestMethod]
        public void ValidateEmptyCollectionNotAllowedField_ShouldNotPassIfNullCollectIsProvided()
        {
            var obj = new ModelWithRequiredNonEmptyCollection();
            obj.List = null;

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }

        [TestMethod]
        public void ValidateEmptyCollectionNotAllowedField_ShouldPassIfNonEmptyCollectIsProvided()
        {
            var obj = new ModelWithRequiredNonEmptyCollection();

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ValidateEmptyCollectionAllowedField_ShouldPassIfEmptyCollectIsProvided()
        {
            var obj = new ModelWithRequiredCollection();

            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsFalse(validation.HasError);
        }

        [TestMethod]
        public void ValidateEmptyCollectionAllowedField_ShouldNotPassIfNullCollectIsProvided()
        {
            var obj = new ModelWithRequiredCollection();
            obj.List = null;
            var validation = ModelValidator.ValidateEntity(obj);
            Assert.IsTrue(validation.HasError);
        }
    }
}
