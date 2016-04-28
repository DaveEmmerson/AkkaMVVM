using Akka.Actor;
using Akka.Event;
using Akka.Pattern;
using AkkaMvvm.Interfaces;
using AkkaMvvm.Messages;
using AkkaMvvm.ViewModels;
using AkkaMvvm.Views;
using System;

namespace AkkaMvvm.Actors
{
    public class MainWindowActor : ReceiveActor
    {
        private IDeadLetterViewModel _deadLetterViewModel;
        private ITickerViewModel _tickerViewModel;
        private ILogViewModel _logViewModel;

        public MainWindowActor()
        {
            _logViewModel = new LogViewModel();
            
            var deadLetterViewModelActor = Context.ActorOf(
                Props.Create(
                    () => new DeadLetterViewModelActor(Self)
                )
            );

            Context.System.EventStream.Subscribe<DeadLetter>(deadLetterViewModelActor);
            
            var logActor = Context.ActorOf(Props.Create(typeof(LogActor), _logViewModel));      

            var childProps = Props.Create<TickerActor>();

            var tickerActor = Context.ActorOf(
                Props.Create(() =>
                    new BackoffSupervisor(childProps, "Ticker", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), 0.1)
                )
            );

            Context.ActorOf(
                Props.Create(
                    () => new TickerViewModelActor(tickerActor, Self)
                )
            );

            Receive<TickerViewModelCreated>(message =>
            {
                _tickerViewModel = message.TickerViewModel;
                if (_deadLetterViewModel != null)
                    CreateMainWindow();
            });

            Receive<DeadLetterViewModelCreated>(message =>
            {
                _deadLetterViewModel = message.DeadLetterViewModel;
                if (_tickerViewModel != null)
                    CreateMainWindow();
            });
        }

        public void CreateMainWindow()
        {
            var mainWindowViewModel = new MainWindowViewModel(_tickerViewModel, _logViewModel, _deadLetterViewModel);
            var mainWindow = new MainWindow();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
