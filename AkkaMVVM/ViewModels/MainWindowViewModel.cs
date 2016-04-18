using AkkaMvvm.Interfaces;

namespace AkkaMvvm.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public ITickerViewModel TickerViewModel { get; }
        public ILogViewModel LogViewModel { get; }
        public IDeadMessagesViewModel DeadMessagesViewModel { get; }

        public MainWindowViewModel(ITickerViewModel tickerViewModel, ILogViewModel logViewModel, IDeadMessagesViewModel deadMessagesViewModel)
        {
            TickerViewModel = tickerViewModel;
            LogViewModel = logViewModel;
            DeadMessagesViewModel = deadMessagesViewModel;
        }
    }
}
