using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Zenith.Client.Shared.Controls
{
    public class DataListContainer: ContentControl
    {
        static DataListContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataListContainer),
                       new FrameworkPropertyMetadata(typeof(DataListContainer)));
        }
    }
}
