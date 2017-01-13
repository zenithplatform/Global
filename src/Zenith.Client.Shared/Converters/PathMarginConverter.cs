using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Zenith.Client.Shared.Converters
{
    [ValueConversion(typeof(Thickness), typeof(Thickness))]
    public class PathMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double pathWidth = 0;
            //FrameworkElement targetElement = (FrameworkElement)value;
            pathWidth = double.Parse(parameter.ToString());
            double offset = (double)value / 2;

            return new Thickness(offset + pathWidth / 2, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
