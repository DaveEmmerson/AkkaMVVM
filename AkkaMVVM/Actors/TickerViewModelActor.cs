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
            Receive<IsRunningMessage>(_ =>
            {
                Running = true;
                Become(Started);
            });
        }

        public void Started() {
            Receive<StopMessage>(_ =>
            {
                Become(Stopping);
                _tickerActor.Tell(new StopMessage());
            });
        }

        public void Stopping() {
            Receive<IsStoppedMessage>(_ =>
            {
                Running = false;
                Become(Stopped);
            });
        }
                                                                                                                                                                                                                                                                                                                         
        public void Stopped() {
            Receive<StartMessage>(_ =>
            {
                Become(Starting);
                Context.ActorSelection("blah").Tell("this should go to dead letters");
                _tickerActor.Tell(new StartMessage());
            });
        }
    }
}
