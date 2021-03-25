using Newtonsoft.Json.Linq;

namespace ModelValidation.Models
{
    /// <summary>
    /// Creates a json validation object from the current model attributes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MVModel<T> where T : class
    {
        public MVModel()
        {
            SetValidationModel();
        }

        private JObject _validation { get; set; }

        public JObject Validation => _validation;

        private void SetValidationModel()
        {
            _validation = AttributesToJson<T>.GetValidationObjectFromAttributes();
        }
    }
}
