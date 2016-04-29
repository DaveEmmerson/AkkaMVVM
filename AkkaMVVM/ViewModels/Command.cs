using System;
using System.Windows.Input;
using System.Windows;
using AkkaMvvm.Utilities;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace AkkaMvvm.ViewModels
{
    public class Command : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;
        private readonly Action<Action> _dispatcherInvoke;

        public Command(Func<object, bool> canExecute, Action<object> execute, [Optional] Action<Action> dispatcherInvoke)
        {
            Guard.NotNull(canExecute);
            Guard.NotNull(execute);

            _canExecute = canExecute;
            _execute = execute;
            _dispatcherInvoke = dispatcherInvoke ?? Application.Current.Dispatcher.Invoke;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            _dispatcherInvoke(() => CanExecuteChanged?.Invoke(this, null));
        }
    }
}