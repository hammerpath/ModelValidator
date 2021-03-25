using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ModelValidation.Validators
{
    public class EntityValidationResult
    {
        public IList<ValidationResult> Errors { get; }
        public bool HasError => Errors.Count > 0;

        public EntityValidationResult(IList<ValidationResult> errors = null)
        {
            Errors = errors ?? new List<ValidationResult>();
        }

        public override string ToString()
        {
            return Errors.Aggregate("Errors:", (s, result) => s += $"\n - {result.ErrorMessage}");
        }
    }

    internal static class EntityValidator
    {
        internal static EntityValidationResult Validate<T>(T entity) where T : class
        {
            var validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(entity, null, null);
            Validator.TryValidateObject
                (entity, vc, validationResults, true);

            return new EntityValidationResult(validationResults);
        }
    }

    public static class ModelValidator
    {
        public static EntityValidationResult ValidateEntity<T>(T entity)
            where T : class
        {
            return EntityValidator.Validate(entity);
        }
    }
}
