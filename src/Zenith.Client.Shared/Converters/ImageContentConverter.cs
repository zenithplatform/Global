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
    public class ImageContentConverter : MarkupExtension, IMultiValueConverter
    {
        private ResourceDictionary _iconResource;
        private string _resourceUri = "";
        private Brush _fillColor = Brushes.Black;

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imageResource = values[0].ToString();
            _fillColor = values[1] as Brush;

            if(imageResource.StartsWith("pack://application"))
            {
                return GetImage(imageResource);
            }
            else
            {
                return ConstructPath(imageResource);
            }
        }

        private Path ConstructPath(string imageResourceKey)
        {
            Canvas iconCanvas = null;

            Path resultPath = new Path();
            resultPath.Fill = _fillColor;
            resultPath.Stretch = Stretch.Uniform;
            
            if (_iconResource == null)
            {
                if (string.IsNullOrEmpty(_resourceUri))
                    iconCanvas = (Canvas)Application.Current.FindResource(imageResourceKey);
                else
                {
                    _iconResource = new ResourceDictionary
                    {
                        Source = new Uri(_resourceUri)
                    };

                    string resourceKey = imageResourceKey;
                    iconCanvas = _iconResource[resourceKey] as Canvas;
                }
            }

            Geometry iconPathData = null;

            if (iconCanvas != null)
            {
                Path iconPath = iconCanvas.Children.Count > 0 ? iconCanvas.Children[0] as Path : null;

                if (iconPath != null)
                {
                    iconPathData = iconPath.Data;
                    resultPath.Data = iconPathData;
                }
            }

            return resultPath;
        }

        private Image GetImage(string imagePath)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(imagePath));
            return image;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string ResourceUri
        {
            get { return _resourceUri; }
            set { _resourceUri = value; }
        }
    }
}
