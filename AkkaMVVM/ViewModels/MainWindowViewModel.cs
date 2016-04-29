using AkkaMvvm.Interfaces;

namespace AkkaMvvm.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public ITickerViewModel TickerViewModel { get; }
        public ILogViewModel LogViewModel { get; }
        public IDeadLettersViewModel DeadLettersViewModel { get; }

        public MainWindowViewModel(ITickerViewModel tickerViewModel, ILogViewModel logViewModel, IDeadLettersViewModel deadLettersViewModel)
        {
            TickerViewModel = tickerViewModel;
            LogViewModel = logViewModel;
            DeadLettersViewModel = deadLettersViewModel;
        }
    }
}
