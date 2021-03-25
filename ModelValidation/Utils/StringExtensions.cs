using System;
using System.Collections.Generic;
using System.Text;

namespace ModelValidation.Utils
{
    internal static class StringExtensions
    {
        internal static string EscapeBackSlash(this string str)
        {
            var position = str.IndexOf("\\", StringComparison.Ordinal);
            if (position >= 0)
            {
                return str.Insert(position, "\\");

            }

            return str;
        }
    }
}
