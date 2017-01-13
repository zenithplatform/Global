using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfMath;

namespace Zenith.Client.Shared.Controls
{
    public class VisualContainerElement : FrameworkElement
    {
        private DrawingVisual visual;
        //private string _textSource = "";
        //private TexFormulaParser _formulaParser;

        public VisualContainerElement()
            : base()
        {
            visual = null;
        }

        public DrawingVisual Visual
        {
            get { return visual; }
            set
            {
                visual = value;
                UpdateVisual();
            }
        }

        private void UpdateVisual()
        {
            RemoveVisualChild(visual);
            AddVisualChild(visual);

            InvalidateMeasure();
            InvalidateVisual();
        }

        private static void UpdateVisual(string sourceText, VisualContainerElement element)
        {
            TexFormulaParser.Initialize();
            TexFormulaParser _formulaParser = new TexFormulaParser();
            // Create formula object from input text.
            TexFormula formula = null;

            try
            {
                formula = _formulaParser.Parse(sourceText);
            }
#if !DEBUG
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while parsing the given input:" + Environment.NewLine +
                    Environment.NewLine + ex.Message, "WPF-Math Example", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
#endif
            finally
            {
            }

            // Render formula to visual.
            var visual = new DrawingVisual();
            var renderer = formula.GetRenderer(TexStyle.Display, 30d);
            var formulaSize = renderer.RenderSize;

            using (var drawingContext = visual.RenderOpen())
            {
                renderer.Render(drawingContext, 0, 1);
            }

            element.Visual = visual;
            //this.Visual = visual;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.visual;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.visual != null)
                return this.visual.ContentBounds.Size;
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        // Dependency Property
        public static readonly DependencyProperty TextSourceProperty =
             DependencyProperty.Register("TextSource", typeof(string),
             typeof(VisualContainerElement), new FrameworkPropertyMetadata("", OnTextSourcePropertyChanged));

        private static void OnTextSourcePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            string str = e.NewValue.ToString();

            if (!string.IsNullOrEmpty(str))
                UpdateVisual(str, (source as VisualContainerElement));
        }

        public string TextSource
        {
            get
            {
                return (string)GetValue(TextSourceProperty);
            }
            set
            {
                SetValue(TextSourceProperty, value);

                //if(GetValue(TextSourceProperty) != null)
                //{
                //    string str = GetValue(TextSourceProperty).ToString();

                //    if(!string.IsNullOrEmpty(str))
                //        UpdateVisual(_textSource);
                //}
            }
        }
    }
}
