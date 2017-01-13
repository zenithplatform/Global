using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zenith.Client.Shared.Controls;

namespace Zenith.Client.Shared.TemplateSelectors
{
    public class ImageContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item == null)
                return IconResourceKeyTemplate;

            ImageContentControl imgControl = item as ImageContentControl;

            string resourcePathOrName = imgControl.ImageResource;

            if (resourcePathOrName.StartsWith("pack://application"))
                return ImageSourceTemplate;
            else
                return IconResourceKeyTemplate;
        }

        public DataTemplate ImageSourceTemplate { get; set; }

        public DataTemplate IconResourceKeyTemplate { get; set; }
    }
}
