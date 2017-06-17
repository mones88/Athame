using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AthameWPF.Utils
{
    public class StringObjectFormatter
    {
        private static readonly Regex FormatRegex = new Regex(@"(?<!{){([\w\d\.]*)}");

        private object GetPropertyValueFromPath(string[] propertyPath, object baseObject)
        {
            for(var i = 0; i < propertyPath.Length; i++)
            {
                var key = propertyPath[i];
                if (Globals.ContainsKey(key)) return Globals[key];

                // Work on the first part of the path
                var objType = baseObject.GetType();
                var baseProperty = objType.GetProperty(key);

                // If we can't find the property, return null
                if (baseProperty == null)
                {
                    return null;
                }

                // If we are at the last element, return the string value
                if (propertyPath.Length - 1 == i)
                {
                    return baseProperty.GetValue(baseObject);
                }

                // If we have more than one element, try again
                var propertyValue = baseProperty.GetValue(baseObject);
                if (propertyValue == null)
                {
                    return null;
                }

                baseObject = propertyValue;
            }
            return null;
        }

        public static string Format(string formatString, object value)
        {
            return Format(formatString, value, null);
        }

        /// <summary>
        /// Returns the string representation of an object, or "null" if the object is null.
        /// </summary>
        public static readonly Func<object, string> DefaultFormatter = o => o?.ToString() ?? "null";

        public static string Format(string formatString, object value, Func<object, string> stringFormatter)
        {
            return new StringObjectFormatter().FormatInstance(formatString, value, stringFormatter);
        }

        public Dictionary<string, object> Globals { get; }

        public StringObjectFormatter()
        {
            Globals = new Dictionary<string, object>();
        }

        public string FormatInstance(string formatString, object value)
        {
            return FormatInstance(formatString, value, null);
        }

        public string FormatInstance(string formatString, object value, Func<object, string> stringFormatter)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (formatString == null)
            {
                throw new ArgumentNullException(nameof(formatString));
            }
            if (stringFormatter == null)
            {
                stringFormatter = DefaultFormatter;
            }

            var matches = FormatRegex.Matches(formatString);
            var tokens = from match in matches.Cast<Match>()
                         select match.Groups[1].Value;
            var replacements = new Dictionary<string, object>();

            foreach (var token in tokens)
            {
                var path = token.Split('.');
                replacements[token] = GetPropertyValueFromPath(path, value);
            }

            return FormatRegex.Replace(formatString, match =>
            {
                var matchToken = match.Groups[1].Value;
                return !replacements.ContainsKey(matchToken) ? match.Value : stringFormatter(replacements[matchToken]);
            });
        }

    }
}
