using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Client.Shared.Interfaces
{
    public interface IModulePresenter
    {
        void Render(ModuleMetadata metadata, IShellCoordinator shellCoordinator);
    }
}
