using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace InsAndOuts.Utilities
{
    public static class StringExtensions
    {
        public static bool IsNullEmptyOrWhitespace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// "HasValue" means the string is:
        ///     NOT Null,
        /// and NOT Empty,
        /// and NOT Whitespace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasValue(this string value)
        {
            return ! IsNullEmptyOrWhitespace(value);
        }

        public static string ToTitleCase(this string value
                                       , bool        force = false)
        {
            var ti = new CultureInfo("en-US"
                                   , false).TextInfo;

            if (force)
            {
                value = value?.ToLower();
            }
            return ti.ToTitleCase(value);
        }
    }
}
