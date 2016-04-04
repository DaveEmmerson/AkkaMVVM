using AkkaMvvm.Interfaces;
using AkkaMvvm.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace AkkaMvvm.ViewModels
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TickerViewModel : ViewModelBase, ITickerViewModel // Need to inherit from one of the Actor types...
        // ... or have an Actor as a separate nested class?
        // ... could the view model be the nested one?
        // ... could implement INotifyPropertyChanged and then use an intance of ViewModelBase?
    {
        #region Properties

        private Command _startCommand;
        public ICommand StartCommand { get { return _startCommand; } }

        private Command _stopCommand;
        public ICommand StopCommand { get { return _stopCommand; } }

        private int _speed;
        public int Speed
        {
            get { _log.Verbose($"Getting speed (is {_speed})"); return _speed; }
            set
            {
                _log.Verbose($"Setting speed to {value}");
                Set(ref _speed, value);
            }
        }

        private bool _running;
        private bool Running
        {
            set
            {
                if (value == _running)
                    return;

                _running = value;
                _startCommand.RaiseCanExecuteChanged();
                _stopCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Fields

        private readonly ILogger _log;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public TickerViewModel(ILogger log)
        {
            Guard.NotNull(log);

            _log = log;

            _startCommand = new Command(
                canExecute: _ =>
                {
                    log.Verbose("CanExecute of _startCommand");
                    return !_running;
                },
                execute: _ =>
                {
                    log.Verbose("Execute of _startCommand - before set running");
                    Running = true;
                    log.Verbose("Execute of _startCommand - after set running");
                    log.Verbose("Execute of _startCommand - before post");

                    Running = false;

                    log.Verbose("Execute of _startCommand - after post");
                }
            );

            _stopCommand = new Command(
                canExecute: _ =>
                {
                    log.Verbose("CanExecute of _stopCommand");
                    return _running;
                },
                execute: _ =>
                {
                    log.Verbose("Execute of _stopCommand - before set running");
                    Running = false;
                    log.Verbose("Execute of _stopCommand - after set running");
                    log.Verbose("Execute of _stopCommand - before post");
                    Running = true;

                    log.Verbose("Execute of _stopCommand - after post");
                }
            );
        }

        #endregion
    }
}
