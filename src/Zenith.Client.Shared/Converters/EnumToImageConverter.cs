using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zenith.Client.Shared.Converters
{
    public abstract class EnumToImageConverterBase : MarkupExtension
    {
        private ResourceDictionary _iconResource;
        private string _resourceUri = "pack://application:,,,/Zenith.Assets;component/Resources/Icons.xaml";
        string resourceKey = "";

        public object OnConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_iconResource == null)
            {
                _iconResource = new ResourceDictionary
                {
                    Source = new Uri(_resourceUri)
                };
            }

            resourceKey = ResourceFromEnum(value);

            Geometry iconPathData = null;
            Canvas iconCanvas = _iconResource[resourceKey] as Canvas;

            if (iconCanvas != null)
            {
                if (iconCanvas.Children.Count > 0)
                {
                    UIElement element = iconCanvas.Children[0];

                    if (element is Path)
                    {
                        Path path = iconCanvas.Children[0] as Path;
                        iconPathData = path.Data;
                    }
                }
            }

            return iconPathData;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        protected abstract string ResourceFromEnum(object enumValue);
    }
}
