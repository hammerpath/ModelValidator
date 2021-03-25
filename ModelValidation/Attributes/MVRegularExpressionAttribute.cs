using System;
using System.Diagnostics.CodeAnalysis;

namespace ModelValidation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MVRegularExpressionAttribute : MVRegularExpressionBaseAttribute
    {
        public MVRegularExpressionAttribute([NotNull] string pattern) : base(pattern)
        {
        }

        public string GetErrorMessage()
        {
            return ErrorMessageString;
        }
    }
}
