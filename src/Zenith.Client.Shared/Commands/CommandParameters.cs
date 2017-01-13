using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zenith.Client.Shared.Commands
{
    public class CommandParameters
    {
        public object InputData { get; set; }
        public FrameworkElement SourceElement { get; set; }
    }
}
