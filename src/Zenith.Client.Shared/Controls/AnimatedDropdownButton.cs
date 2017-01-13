using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace Zenith.Client.Shared.Controls
{
    public class AnimatedDropdownButton : Button
    {
        //bool _isClicked = false;
        //bool _contextMenuOpened = false;

        static AnimatedDropdownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedDropdownButton),
                new FrameworkPropertyMetadata(typeof(AnimatedDropdownButton)));
        }

        public AnimatedDropdownButton()
        {
            //this.Click += AnimatedDropdownButton_Click;
            //this.MouseEnter += AnimatedDropdownButton_MouseEnter;
            //this.MouseLeave += AnimatedDropdownButton_MouseLeave;
            //this.Checked += AnimatedDropdownButton_Checked;
            //this.LostFocus += AnimatedDropdownButton_LostFocus;
        }

        //private void AnimatedDropdownButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    Storyboard sb = this.FindResource("CollapseExtraContent") as Storyboard;
        //    var template = this.Template;
        //    var myControl = (Border)template.FindName("ExpandableRegion", this);
        //    sb.Begin(myControl);
        //}

        //private void AnimatedDropdownButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    Storyboard sb = this.FindResource("ExpandExtraContent") as Storyboard;
        //    var template = this.Template;
        //    var myControl = (Border)template.FindName("ExpandableRegion", this);
        //    sb.Begin(myControl);
        //}

        //private void AnimatedDropdownButton_Click(object sender, RoutedEventArgs e)
        //{
        //    _isClicked = true;

        //    var template = this.Template;
        //    var myControl = (Border)template.FindName("ExpandableRegion", this);

        //    if(myControl != null)
        //    {
        //        this.ContextMenu.IsOpen = true;
        //        this.ContextMenu.Closed += ContextMenu_Closed;

        //        Storyboard sb = this.FindResource("CollapseExtraContent") as Storyboard;
        //        //Storyboard.SetTarget(sb, myControl);
        //        //Storyboard.SetTargetProperty(myControl, new PropertyPath("Tag"));
        //        sb.Stop();
        //        //myControl.Width = Double.NaN;
        //    }
        //}

        //private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        //{
        //    _isClicked = false;

        //    var template = this.Template;
        //    var myControl = (Border)template.FindName("ExpandableRegion", this);

        //    if (myControl != null)
        //    {
        //        Storyboard sb = this.FindResource("CollapseExtraContent") as Storyboard;
        //        //Storyboard.SetTarget(sb, myControl);
        //        //Storyboard.SetTargetProperty(myControl, new PropertyPath("Tag"));
        //        sb.Resume();

        //        this.ContextMenu.Closed -= ContextMenu_Closed;
        //        //myControl.Width = 0;

        //        //if (_isClicked)
        //        //{
        //        //    this.ContextMenu.IsOpen = true;
        //        //    this.ContextMenu.Closed += ContextMenu_Closed;

        //        //}
        //        //else
        //        //{

        //        //}
        //    }
        //}

        //private void ContextMenu_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        //{
        //    this.IsChecked = false;
        //}

        //private void AnimatedDropdownButton_LostFocus(object sender, RoutedEventArgs e)
        //{

        //}

        //private void AnimatedDropdownButton_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (this.IsChecked.Value)
        //    {
        //        this.ContextMenu.ContextMenuClosing -= ContextMenu_ContextMenuClosing;
        //        this.ContextMenu.IsOpen = true;
        //        this.ContextMenu.ContextMenuClosing += ContextMenu_ContextMenuClosing;
        //    }
        //    //else
        //    //{

        //    //}
        //    //if (this.ContextMenu.IsOpen)
        //    //{

        //    //}
        //    //
        //}

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, (string)value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string),
              typeof(AnimatedDropdownButton), new PropertyMetadata(null));
    }
}
