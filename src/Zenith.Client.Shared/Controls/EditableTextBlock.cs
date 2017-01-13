using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zenith.Client.Shared.Controls
{
    public class EditableTextBlock : TextBox
    {
        static EditableTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(EditableTextBlock),
                new FrameworkPropertyMetadata(typeof(EditableTextBlock)));
        }

        public static DependencyProperty TextBlockContentProperty =
            DependencyProperty.Register(
                "TextBlockContent",
                typeof(string),
                typeof(EditableTextBlock));

        public string TextBlockContent
        {
            get { return (string)GetValue(TextBlockContentProperty); }
            set { SetValue(TextBlockContentProperty, value); }
        }

        public static DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register(
                "ButtonCommand",
                typeof(ICommand),
                typeof(EditableTextBlock));

        public ICommand ButtonCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }
    }
}
