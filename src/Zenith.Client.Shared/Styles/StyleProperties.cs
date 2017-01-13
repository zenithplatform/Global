using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Zenith.Client.Shared.Styles
{
    public static class StyleProperties
    {
        public static Brush GetMouseOverBackgroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBackgroundBrushProperty);
        }

        public static void SetMouseOverBackgroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty MouseOverBackgroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "MouseOverBackgroundBrush",
                typeof(Brush),
                typeof(StyleProperties),
                new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBackgroundBrushProperty);
        }

        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached(
                "MouseOverBorderBrush",
                typeof(Brush),
                typeof(StyleProperties),
                new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetPressedBackgroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedBackgroundBrushProperty);
        }

        public static void SetPressedBackgroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty PressedBackgroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "PressedBackgroundBrush",
                typeof(Brush),
                typeof(StyleProperties),
                new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetRectangleHighlightBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RectangleHighlightBrushProperty);
        }

        public static void SetRectangleHighlightBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(RectangleHighlightBrushProperty, value);
        }

        public static readonly DependencyProperty RectangleHighlightBrushProperty =
            DependencyProperty.RegisterAttached(
                "RectangleHighlightBrush",
                typeof(Brush),
                typeof(StyleProperties),
                new FrameworkPropertyMetadata(Brushes.Black));

        public static Brush GetRectangleNormalBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RectangleNormalBrushProperty);
        }

        public static void SetRectangleNormalBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(RectangleNormalBrushProperty, value);
        }

        public static readonly DependencyProperty RectangleNormalBrushProperty =
            DependencyProperty.RegisterAttached(
                "RectangleNormalBrush",
                typeof(Brush),
                typeof(StyleProperties),
                new FrameworkPropertyMetadata(Brushes.Black));
    }
}
