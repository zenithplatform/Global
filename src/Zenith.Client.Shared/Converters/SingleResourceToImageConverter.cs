using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zenith.Client.Shared.Converters
{
    public class SingleResourceToImageConverter :  IValueConverter
    {
        private Brush _fillColor = Brushes.Black;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imageResource = value.ToString();

            if (imageResource.StartsWith("pack://application"))
            {
                return GetImage(imageResource);
            }
            else
            {
                return (Canvas)Application.Current.FindResource(imageResource);
            }
        }

        private Image GetImage(string imagePath)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(imagePath));
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
