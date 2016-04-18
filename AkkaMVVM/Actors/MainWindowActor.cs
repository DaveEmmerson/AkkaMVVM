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
            var deadMessagesViewModel = new DeadMessagesViewModel();
            var logActor = Context.ActorOf(Props.Create(() => new LogActor(logViewModel)));
            logActor.Tell(new Debug(nameof(MainWindowActor), typeof(MainWindowActor), "Logger started"));

            var childProps = Props.Create<TickerActor>();

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
                var mainWindowViewModel = new MainWindowViewModel(message.TickerViewModel, logViewModel, deadMessagesViewModel);
                var mainWindow = new MainWindow();

                mainWindow.DataContext = mainWindowViewModel;

                mainWindow.Show();
                Context.GetLogger().Debug("Main window shown");
            });
        }
    }
}
