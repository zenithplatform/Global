using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Client.Shared.Interfaces;

namespace Zenith.Client.Shared.ViewsModels
{
    public class ExposableViewModelBase : ViewModelBase, IShellElementTarget
    {
        IShellCoordinator _shellCoordinator = null;
        private Dictionary<string, Action<object>> _actions = new Dictionary<string, Action<object>>();

        public ExposableViewModelBase(IShellCoordinator shellCoordinator)
        {
            this._shellCoordinator = shellCoordinator;
        }

        protected virtual void RegisterGlobalAction(string actionKey, Action<object> action)
        {
            if(!_actions.ContainsKey(actionKey))
                _actions.Add(actionKey, action);

            _shellCoordinator.Register(this, actionKey);
        }

        protected virtual void UnregisterGlobalAction(string actionKey, Action<object> action)
        {
            if(_actions.ContainsKey(actionKey))
                _actions.Remove(actionKey);

            _shellCoordinator.Unregister(this, actionKey);
        }

        public void InvokeAction(string actionKey, object payload)
        {
            if (_actions.ContainsKey(actionKey))
            {
                Action<object> target = _actions[actionKey];
                target.Invoke(payload);
            }
        }
    }
}
