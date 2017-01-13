using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shapes;

namespace Zenith.Client.Shared.Windows
{
    public class PopupWindow : Window
    {
        bool _isPinned = false;

        static PopupWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupWindow),
                new FrameworkPropertyMetadata(typeof(PopupWindow)));
        }

        public PopupWindow()
            : base()
        {
            PreviewMouseMove += OnPreviewMouseMove;
        }

        protected void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        #region Click events

        protected void PinClick(object sender, RoutedEventArgs e)
        {
            _isPinned = !_isPinned;
            PopupWindow.SetIsPinned(this, _isPinned);
        }

        protected void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        public override void OnApplyTemplate()
        {
            Button pinButton = GetTemplateChild("pinButton") as Button;
            if (pinButton != null)
                pinButton.Click += PinClick;

            Button closeButton = GetTemplateChild("closeButton") as Button;
            if (closeButton != null)
                closeButton.Click += CloseClick;

            TextBlock titleBar = GetTemplateChild("titleBar") as TextBlock;
            if (titleBar != null)
            {
                titleBar.PreviewMouseDown += titleBar_PreviewMouseDown;
            }

            Grid resizeGrid = GetTemplateChild("resizeGrid") as Grid;
            if (resizeGrid != null)
            {
                foreach (UIElement element in resizeGrid.Children)
                {
                    Rectangle resizeRectangle = element as Rectangle;
                    if (resizeRectangle != null)
                    {
                        resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                        resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                    }
                }
            }

            base.OnApplyTemplate();
        }

        private void titleBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (!_isPinned)
                    DragMove();
            }
        }

        protected void ResizeRectangle_MouseMove(Object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    break;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;      // width of left border that retains its size
            public int cxRightWidth;     // width of right border that retains its size
            public int cyTopHeight;      // height of top border that retains its size
            public int cyBottomHeight;   // height of bottom border that retains its size
        };

        protected void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
                default:
                    break;
            }
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        private HwndSource _hwndSource;

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += OnSourceInitialized;
            base.OnInitialized(e);
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        public static readonly DependencyProperty IsPinned = DependencyProperty.RegisterAttached("IsPinned", typeof(Boolean), typeof(PopupWindow), new PropertyMetadata(false, new PropertyChangedCallback(WindowPinnedChanged)));

        private static void WindowPinnedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PopupWindow popup = (PopupWindow)d;

            if ((bool)e.NewValue)
                popup.Topmost = true;
            else
                popup.Topmost = false;
        }

        public static void SetIsPinned(UIElement element, Boolean value)
        {
            element.SetValue(IsPinned, value);
        }

        public static Boolean GetIsPinned(UIElement element)
        {
            return (Boolean)element.GetValue(IsPinned);
        }

        //public void DropShadowToWindow()
        //{
        //    if (!DropShadow(this))
        //    {
        //        SourceInitialized += OnSourceInitialized;
        //    }
        //}

        /// <summary>
        /// The actual method that makes API calls to drop the shadow to the window
        /// </summary>
        /// <param name="window">Window to which the shadow will be applied</param>
        /// <returns>True if the method succeeded, false if not</returns>
        //private bool DropShadow(Window window)
        //{
        //    try
        //    {
        //        WindowInteropHelper helper = new WindowInteropHelper(window);
        //        int val = 2;
        //        int ret1 = DwmSetWindowAttribute(helper.Handle, 2, ref val, 4);

        //        if (ret1 == 0)
        //        {
        //            MARGINS m = new MARGINS { cyBottomHeight = 0, cxLeftWidth = 0, cxRightWidth = 0, cyTopHeight = 0 };
        //            int ret2 = DwmExtendFrameIntoClientArea(helper.Handle, ref m);
        //            return ret2 == 0;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Probably dwmapi.dll not found (incompatible OS)
        //        return false;
        //    }
        //}
    }
}
