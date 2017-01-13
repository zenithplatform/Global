using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zenith.Client.Shared.Converters
{
    public class KeyToResourceConverter : MarkupExtension, IValueConverter
    {
        private ResourceDictionary _iconResource;
        private string _resourceUri = "";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(_iconResource == null)
            {
                if (string.IsNullOrEmpty(_resourceUri))
                    return Application.Current.FindResource(value as string);
                else
                {
                    _iconResource = new ResourceDictionary
                    {
                        Source = new Uri(_resourceUri)
                    };
                }
            }

            Geometry iconPathData = null;
            string resourceKey = value.ToString();
            Canvas iconCanvas = _iconResource[resourceKey] as Canvas;

            if (iconCanvas != null)
            {
                Path iconPath = iconCanvas.Children.Count > 0 ? iconCanvas.Children[0] as Path : null;

                if (iconPath != null)
                {
                    iconPathData = iconPath.Data;
                }
            }

            return iconPathData;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public string ResourceUri
        {
            get { return _resourceUri; }
            set { _resourceUri = value; }
        }
    }
}
