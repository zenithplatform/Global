using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zenith.Client.Shared.Windows
{
    public partial class MetroDialog : MetroWindow
    {
        public MetroDialog()
        {
            InitializeComponent();
            base.ShowMaxRestoreButton = false;
            base.ShowMinButton = false;
            base.Topmost = true;
        }
    }
}
