using AkkaMvvm.Interfaces;
using System.Windows.Input;
using System.Timers;
using Akka.Actor;
using System;

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
            set { Set(ref _speed, value); }
        }

        public TickerViewModel()
        {
            StartCommand = new Command(
                canExecute: _ => true, //!actor.IsRunning,
                execute: _ => { }// actorRef.Tell(new StartMessage())
            );

            StopCommand = new Command(
                canExecute: _ => true, //actor.IsRunning,
                execute: _ => { }// actorRef.Tell(new StopMessage())
            );
        }
    }
}
