using AkkaMvvm.Interfaces;
using System.Windows.Input;
using Akka.Actor;
using AkkaMvvm.Messages;
using AkkaMvvm.ViewModels;

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
                Set(ref _running, value, () =>
                {
                    StartCommand.RaiseCanExecuteChanged();
                    StopCommand.RaiseCanExecuteChanged();
                });
            }
        }

        private IActorRef _tickerActor;

        public TickerViewModelActor(IActorRef tickerActor, IActorRef parent)
        {
            _tickerActor = tickerActor;
            Become(Stopped);
            var context = Context;

            StartCommand = new Command(
                canExecute: _ => !Running,
                execute: _ => {
                    // can't become here because the executing thread is the UI thread, not an Akka thread with a Context.
                    Become(Starting);
                    tickerActor.Tell(new StartMessage());
                }
            );

            StopCommand = new Command(
                canExecute: _ => Running,
                execute: _ =>
                {
                    Become(Stopping);
                    tickerActor.Tell(new StopMessage());
                }
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

        public void Started() { }

        public void Stopping() {
            Receive<IsStoppedMessage>(_ =>
           {
               Running = false;
               Become(Stopped);
           });
        }

        public void Stopped() { }
    }
}
