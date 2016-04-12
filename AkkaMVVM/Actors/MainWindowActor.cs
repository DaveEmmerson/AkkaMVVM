using Akka.Actor;
using Akka.Event;
using Akka.Pattern;
using AkkaMvvm.ViewModels;
using AkkaMvvm.Views;
using System;

namespace AkkaMvvm.Actors
{
    public class MainWindowActor : ReceiveActor
    {

        public MainWindowActor()
        {
            var logViewModel = new LogViewModel();
            var logActor = Context.ActorOf(Props.Create(() => new LogActor(logViewModel)));
            logActor.Tell(new Debug(nameof(MainWindowActor), typeof(MainWindowActor), "Logger started"));

            var childProps = Props.Create(factory: () => new TickerActor(logActor));

            var tickerActor = Context.ActorOf(
                Props.Create(() =>
                    new BackoffSupervisor(childProps, "Ticker", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), 0.1)
                )
            );

            var tickerViewModelActor = Context.ActorOf(
                Props.Create(
                    () => new TickerViewModelActor(tickerActor, Self, logActor)
                )
            );

            Receive<TickerViewModelCreated>(message =>
            {
                var mainWindowViewModel = new MainWindowViewModel(message.TickerViewModel, logViewModel);
                var mainWindow = new MainWindow();

                logActor.Tell(new Debug(nameof(Receive), typeof(MainWindowActor), "Main window created"));
                mainWindow.DataContext = mainWindowViewModel;

                mainWindow.Show();
                logActor.Tell(new Debug(nameof(Receive), typeof(MainWindowActor), "Main window shown"));
            });
        }
    }
}
