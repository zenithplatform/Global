using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zenith.Client.Shared.Util
{
    public class SystemTray
    {
        System.Windows.Forms.NotifyIcon _notifyIcon;
        Window _targetWindow = null;
        bool _isActive = false;
        System.Drawing.Bitmap _bitmapIcon = null;

        public SystemTray(Window targetWindow, System.Drawing.Bitmap bitmapIcon)
        {
            this._targetWindow = targetWindow;
            this._bitmapIcon = bitmapIcon;
            this._targetWindow.Closing += _targetWindow_Closing;
        }

        private void _targetWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isActive)
            {
                e.Cancel = true;
                this._targetWindow.Hide();
            }
            else
                e.Cancel = false;
        }

        void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this._targetWindow.IsVisible)
                this._targetWindow.Activate();
            else
                this._targetWindow.Show();
        }

        public bool IsActive
        {
            get { return _isActive; }
        }

        public void Deactivate()
        {
            _isActive = false;

            if (_notifyIcon != null)
            {
                _notifyIcon.DoubleClick -= _notifyIcon_DoubleClick;
                _notifyIcon.Icon.Dispose();
                _notifyIcon.Icon = null;
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }

        public void Activate()
        {
            Deactivate();

            _isActive = true;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += _notifyIcon_DoubleClick;
            _notifyIcon.Icon = System.Drawing.Icon.FromHandle(this._bitmapIcon.GetHicon());
            _notifyIcon.Visible = true;
        }
    }
}
