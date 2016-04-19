using AkkaMvvm.Interfaces;

namespace AkkaMvvm.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public ITickerViewModel TickerViewModel { get; }
        public ILogViewModel LogViewModel { get; }
        public IDeadLetterViewModel DeadMessagesViewModel { get; }

        public MainWindowViewModel(ITickerViewModel tickerViewModel, ILogViewModel logViewModel, IDeadLetterViewModel deadMessagesViewModel)
        {
            TickerViewModel = tickerViewModel;
            LogViewModel = logViewModel;
            DeadMessagesViewModel = deadMessagesViewModel;
        }
    }
}
