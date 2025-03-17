using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfClient.Resources.Coverter
{
    class AddLeftPaddingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ( !(value is Thickness padding))
            {
                return value;
            }
            if (!double.TryParse(parameter.ToString(), out double amount))
            {
                return value;
            }
            padding.Left += amount;
            return padding;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
