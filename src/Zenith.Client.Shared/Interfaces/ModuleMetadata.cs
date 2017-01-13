using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zenith.Client.Shared.Interfaces
{
    public abstract class ModuleMetadata
    {
        protected abstract void InitializeStartPage();
        protected abstract void InitializeVisualElements();

        public virtual void Initialize()
        {
            InitializeStartPage();
            InitializeVisualElements();
        }

        public abstract UserControl StartPage { get; }
        public abstract VisualElements VisualElements { get; }
    }

    public struct VisualElements
    {
        public VisualElements(string icon, string caption, string helpText)
        {
            this.Icon = icon;
            this.Caption = caption;
            this.HelpText = helpText;
        }

        public string Icon { get; set; }
        public string Caption { get; set; }
        public string HelpText { get; set; }
    }
}
