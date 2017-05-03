using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace AthameWPF.UI
{
    [ValueConversion(typeof(string), typeof(string))]
    public class StringCaseConverter : IValueConverter
    {
        public CharacterCasing Case { get; set; }

        public StringCaseConverter()
        {
            Case = CharacterCasing.Normal;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (str == null) return string.Empty;
            switch (Case)
            {
                case CharacterCasing.Lower:
                    return str.ToLower();
                case CharacterCasing.Normal:
                    return str;
                case CharacterCasing.Upper:
                    return str.ToUpper();
                default:
                    return str;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
