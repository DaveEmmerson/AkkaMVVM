using Akka.Actor;
using AkkaMvvm.Actors;
using System.Windows;

namespace AkkaMvvm.App
{
    public partial class App : Application
    {
        private const int retryCount = 1;
        private const int retryInterval = 2;

        public App()
        {
            Startup += App_Startup;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
            MessageBox.Show("Exiting application");
            Current.Shutdown();
            e.Handled = true;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var system = ActorSystem.Create("Ticker");

            var mainWindowActor = system.ActorOf(
                Props.Create(
                    () => new MainWindowActor()
                ).WithDispatcher("akka.actor.synchronized-dispatcher")
            );
        }
    }
}
