using AkkaMvvm.Interfaces;

namespace AkkaMvvm.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public ITickerViewModel TickerViewModel { get; }
        public ILogViewModel LogViewModel { get; }

        public MainWindowViewModel(ITickerViewModel tickerViewModel, ILogViewModel logViewModel)
        {
            TickerViewModel = tickerViewModel;
            LogViewModel = logViewModel;
        }
    }
}
