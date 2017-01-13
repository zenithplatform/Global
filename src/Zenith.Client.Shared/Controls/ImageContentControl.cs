using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zenith.Client.Shared.Controls
{
    public class ImageContentControl:ContentControl
    {
        static ImageContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageContentControl),
                new FrameworkPropertyMetadata(typeof(ImageContentControl)));
        }

        public string ImageResource
        {
            get { return (string)GetValue(ImageResourceProperty); }
            set { SetValue(ImageResourceProperty, (string)value); }
        }

        public static readonly DependencyProperty ImageResourceProperty =
            DependencyProperty.Register("ImageResource", typeof(string),
              typeof(ImageContentControl), new PropertyMetadata(string.Empty, null));

        public Brush FillColor
        {
            get { return (Brush)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, (Brush)value); }
        }

        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Brush),
              typeof(ImageContentControl), new PropertyMetadata(Brushes.Black, null));

        public Brush MouseOverColor
        {
            get { return (Brush)GetValue(MouseOverColorProperty); }
            set { SetValue(MouseOverColorProperty, (Brush)value); }
        }

        public static readonly DependencyProperty MouseOverColorProperty =
            DependencyProperty.Register("MouseOverColor", typeof(Brush),
              typeof(ImageContentControl), new PropertyMetadata(Brushes.Black, null));

        //public string Caption
        //{
        //    get { return (string)GetValue(CaptionProperty); }
        //    set { SetValue(CaptionProperty, (string)value); }
        //}

        //public static readonly DependencyProperty CaptionProperty =
        //    DependencyProperty.Register("Caption", typeof(string),
        //      typeof(ImageContentControl), new PropertyMetadata(string.Empty, null));
    }
}
