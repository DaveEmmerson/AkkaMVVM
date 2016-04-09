using AkkaMvvm.Interfaces;
using System.Windows.Input;
using System.Timers;
using Akka.Actor;
using System;
using AkkaMvvm.Actors;
using AkkaMvvm.Messages;

namespace AkkaMvvm.ViewModels
{
    public class TickerViewModel : ViewModelBase, ITickerViewModel
    {
        public Command StartCommand { get; }
        ICommand ITickerViewModel.StartCommand => StartCommand;

        public Command StopCommand { get; }
        ICommand ITickerViewModel.StopCommand => StopCommand;

        private int _speed;
        public int Speed
        {
            get { return _speed; }
            set {
                Set(ref _speed, value, () => _tickerActor.Tell(new ChangeSpeedMessage(value)));
            }
        }

        private IActorRef _tickerActor;

        public TickerViewModel(IActorRef tickerActor)
        {
            _tickerActor = tickerActor;

            StartCommand = new Command(
                canExecute: _ => true, // TODO: !actor.IsRunning
                execute: _ => tickerActor.Tell(new StartMessage())
            );

            StopCommand = new Command(
                canExecute: _ => true, // TODO: actor.IsRunning,
                execute: _ =>  tickerActor.Tell(new StopMessage())
            );
        }
    }
}
