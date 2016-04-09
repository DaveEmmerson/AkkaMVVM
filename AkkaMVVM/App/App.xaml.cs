using Akka.Actor;
using Akka.Event;
using Akka.Pattern;
using AkkaMvvm.Actors;
using AkkaMvvm.Interfaces;
using AkkaMvvm.ViewModels;
using AkkaMvvm.Views;
using System;
using System.Timers;
using System.Windows;

namespace AkkaMvvm.App
{
    public partial class App : Application
    {
        #region Constants

        private const int retryCount = 1;
        private const int retryInterval = 2;

        #endregion Constants

        #region Fields

        private MainWindow _mainWindow;

        #endregion Fields

        public App()
        {
            Startup += App_Startup;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // let's just display a message and carry on regardless... yee haa! :D

            var window = _mainWindow;

            if (window == null || window.Visibility == Visibility.Hidden)
            {
                MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
                MessageBox.Show("Exception occurred before window available. Exiting application");
                Current.Shutdown();
            }
            else
            {
                MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
            }

            e.Handled = true;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var system = ActorSystem.Create("Ticker");

            var logViewModel = new LogViewModel();
            var logActor = system.ActorOf(Props.Create(() => new LogActor(logViewModel)));

            var childProps = Props.Create(factory: () => new TickerActor(logActor));

            var tickerActor = system.ActorOf(
                Props.Create(() =>
                    new BackoffSupervisor(childProps, "Ticker", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), 0.1)
                )
            );

            var tickerViewModel = new TickerViewModel(tickerActor);

            var mainWindowViewModel = new MainWindowViewModel(tickerViewModel, logViewModel);
            _mainWindow = new MainWindow();
            _mainWindow.DataContext = mainWindowViewModel;
            _mainWindow.Show();
        }
    }
}
