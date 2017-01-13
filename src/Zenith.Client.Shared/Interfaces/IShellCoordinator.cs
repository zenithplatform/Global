using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Zenith.Client.Shared.Interfaces
{
    public delegate void ShellEventHandler(ShellEventType eventType, object payload);

    public interface IShellCoordinator
    {
        event ShellEventHandler OnShellEvent;

        Window MainWindow { get; set; }
        void ShowDialog<T>(Window parent, object dataContext) where T : IDialogTarget;
        void CloseDialog<T>() where T : IDialogTarget;
        void Register<T>(T target, string actionKey) where T : IShellElementTarget;
        void Unregister<T>(T target, string action) where T : IShellElementTarget;
        void Unregister<T>(T target) where T : IShellElementTarget;
        void Invoke<T>(string actionKey, object payload) where T : IShellElementTarget;
    }

    public enum ShellEventType
    {
       
    }

    public interface IShellElementTarget
    {
        void InvokeAction(string actionKey, object payload);
    }
}
