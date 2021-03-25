using System;
using System.Collections.Generic;
using System.Text;

namespace ModelValidation.Utils
{
    internal static class ObjectExtensions
    {
        internal static object GetDefaultValue(this Type t)
        {
            if (!t.IsValueType || Nullable.GetUnderlyingType(t) != null)
                return null;
            return Activator.CreateInstance(t);
        }
    }
}
