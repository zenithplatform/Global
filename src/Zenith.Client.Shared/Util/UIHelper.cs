using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Zenith.Client.Shared.Windows;

namespace Zenith.Client.Shared.Util
{
    public class UIHelper
    {
        internal static void ShowPopupWindow(PopupWindow popupWindow, Size windowSize, FrameworkElement positioningElement, object inputData, object dataContext)
        {
            //DetailsPopup details = new DetailsPopup();
            popupWindow.DataContext = dataContext;
            popupWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            popupWindow.ShowInTaskbar = false;
            popupWindow.Width = windowSize.Width;
            popupWindow.Height = windowSize.Height;
            //ObservationItemDetails detailsCtrl = new ObservationItemDetails();
            //detailsCtrl.Height = double.NaN;
            //detailsCtrl.Width = double.NaN;
            //detailsCtrl.HorizontalAlignment = HorizontalAlignment.Stretch;
            //detailsCtrl.VerticalAlignment = VerticalAlignment.Stretch;
            //details.Content = detailsCtrl;
            var location = positioningElement.PointToScreen(new Point(0, 0));
            popupWindow.Left = location.X;
            popupWindow.Top = location.Y + positioningElement.Height + 2;
            popupWindow.Show();
        }
    }
}
