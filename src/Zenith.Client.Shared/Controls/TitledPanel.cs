using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Zenith.Client.Shared.Controls
{
    public class TitledPanel:ContentControl
    {
        static TitledPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitledPanel),
                new FrameworkPropertyMetadata(typeof(TitledPanel)));
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, (string)value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string),
              typeof(TitledPanel), new PropertyMetadata(null));

        //private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    d.SetValue(TitleProperty, e.NewValue);
        //}

        //public object PanelContent
        //{
        //    get { return (object)GetValue(PanelContentProperty); }
        //    set { SetValue(PanelContentProperty, (object)value); }
        //}

        //public static readonly DependencyProperty PanelContentProperty =
        //    DependencyProperty.Register("PanelContent", typeof(object),
        //      typeof(TitledPanel), new PropertyMetadata(null));

        //private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    d.SetValue(ContainerContentProperty, e.NewValue);
        //}
    }
}
