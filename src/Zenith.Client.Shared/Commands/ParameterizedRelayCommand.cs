using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zenith.Client.Shared.Commands
{
    public class ParameterizedRelayCommand<T> : ICommand where T : CommandParameters
    {
        private Predicate<T> _canExecute;
        private Action<T> _execute;

        public ParameterizedRelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        private void Execute(T parameter)
        {
            _execute(parameter);
        }

        private bool CanExecute(T parameter)
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var temp = Volatile.Read(ref CanExecuteChanged);

            if (temp != null)
                temp(this, new EventArgs());
        }
    }

    //class RelayCommands : ICommand
    //{
    //    readonly Action<object> _execute;
    //    readonly Predicate<object> _canExecute;
    //    /// <summary>
    //    /// Constructer takes Execute events to register in CommandManager.
    //    /// </summary>
    //    /// <param name="execute">Execute method as action.</param>
    //    public RelayCommands(Action<object> execute)
    //        : this(execute, null)
    //    {
    //        try
    //        {
    //            if (null == execute)
    //            {
    //                throw new NotImplementedException("Not implemented");
    //            }
    //            _execute = execute;
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //    }
    //    /// <summary>
    //    /// Constructer takes Execute and CanExcecute events to register in CommandManager.
    //    /// </summary>
    //    /// <param name="execute">Execute method as action.</param>
    //    /// <param name="canExecute">CanExecute method as return bool type.</param>
    //    public RelayCommands(Action<object> execute, Predicate<object> canExecute)
    //    {
    //        try
    //        {
    //            if (null == execute)
    //            {
    //                _execute = null;
    //                throw new NotImplementedException("Not implemented");
    //            }
    //            _execute = execute;
    //            _canExecute = canExecute;
    //        }
    //        catch (Exception)
    //        {
    //        }
    //    }
    //    /// <summary>
    //    /// Can Executed Changed Event
    //    /// </summary>
    //    public event EventHandler CanExecuteChanged
    //    {
    //        add
    //        {
    //            CommandManager.RequerySuggested += value;
    //        }
    //        remove
    //        {
    //            CommandManager.RequerySuggested -= value;
    //        }
    //    }
    //    /// <summary>
    //    /// Execute method.
    //    /// </summary>
    //    /// <param name="parameter">Method parameter.</param>
    //    public void Execute(object parameter)
    //    {
    //        _execute(parameter);
    //    }
    //    /// <summary>
    //    /// CanExecute method.
    //    /// </summary>
    //    /// <param name="parameter">Method parameter.</param>
    //    /// <returns>Return true if can execute.</returns>
    //    public bool CanExecute(object parameter)
    //    {
    //        return _canExecute == null || _canExecute(parameter);
    //    }
    //    /// <summary>
    //    /// InvalidateCanExecute method will initiate validation of the Command.
    //    /// </summary>
    //    private void InvalidateCanExecute()
    //    {
    //        CommandManager.InvalidateRequerySuggested();
    //    }
    //}

    public class SimpleRelayCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _execute;

        public SimpleRelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        //private void Execute(object parameter)
        //{
        //    _execute(parameter);
        //}

        //private bool CanExecute(object parameter)
        //{
        //    return true;
        //}

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute((object)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var temp = Volatile.Read(ref CanExecuteChanged);

            if (temp != null)
                temp(this, new EventArgs());
        }
    }
}
