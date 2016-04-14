using AkkaMvvm.Interfaces;
using System.Windows.Input;
using Akka.Actor;
using AkkaMvvm.Messages;
using AkkaMvvm.ViewModels;
using Akka.Event;

namespace AkkaMvvm.Actors
{
    public class TickerViewModelActor : ActorViewModelBase, ITickerViewModel
    {
        public Command StartCommand { get; }
        ICommand ITickerViewModel.StartCommand => StartCommand;

        public Command StopCommand { get; }
        ICommand ITickerViewModel.StopCommand => StopCommand;

        private int _speed;
        public int Speed
        {
            get { return _speed; }
            set
            {
                Set(ref _speed, value, () => _tickerActor.Tell(new ChangeSpeedMessage(value)));
            }
        }

        private bool _running;
        public bool Running
        {
            get { return _running; }
            set
            {
                _log.Tell(new Debug(nameof(Running), typeof(TickerViewModelActor), $"Running was {_running}, now {value}"));
                Set(ref _running, value, () =>
                {
                    StartCommand.RaiseCanExecuteChanged();
                    StopCommand.RaiseCanExecuteChanged();
                });
            }
        }

        private IActorRef _tickerActor;
        private IActorRef _log;

        public TickerViewModelActor(IActorRef tickerActor, IActorRef parent, IActorRef log)
        {
            log.Tell(new Debug(nameof(TickerViewModelActor), typeof(TickerViewModelActor), "Creating TickerViewModelActor"));

            _tickerActor = tickerActor;
            _log = log;
            _tickerActor.Tell(new ChangeSpeedMessage(Speed));
            Become(Stopped);
            var self = Self;

            StartCommand = new Command(
                canExecute: _ => !Running,
                execute: _ => self.Tell(new StartMessage())
            );

            StopCommand = new Command(
                canExecute: _ => Running,
                execute: _ => self.Tell(new StopMessage())
            );

            parent.Tell(new TickerViewModelCreated(this));
        }

        public void Starting()
        {
            _log.Tell(new Debug(nameof(Starting), typeof(TickerViewModelActor), "Became"));
            Receive<IsRunningMessage>(_ =>
            {
                _log.Tell(new Debug(nameof(Receive), typeof(TickerViewModelActor), "IsRunningMessage"));
                Running = true;
                Become(Started);
            });
        }

        public void Started() {
            _log.Tell(new Debug(nameof(Started), typeof(TickerViewModelActor), "Became"));
            Receive<StopMessage>(_ =>
            {
                _log.Tell(new Debug(nameof(Receive), typeof(TickerViewModelActor), "StopMessage"));
                Become(Stopping);
                _tickerActor.Tell(new StopMessage());
            });
        }

        public void Stopping() {
            _log.Tell(new Debug(nameof(Stopping), typeof(TickerViewModelActor), "Became"));
            Receive<IsStoppedMessage>(_ =>
            {
                _log.Tell(new Debug(nameof(Receive), typeof(TickerViewModelActor), "IsStoppedMessage"));
                Running = false;
                Become(Stopped);
            });
        }

        public void Stopped() {
            _log.Tell(new Debug(nameof(Stopped), typeof(TickerViewModelActor), "Became"));
            Receive<StartMessage>(_ =>
            {
                _log.Tell(new Debug(nameof(Receive), typeof(TickerViewModelActor), "StartMessage"));
                Become(Starting);
                _tickerActor.Tell(new StartMessage());
            });
        }
    }
}
