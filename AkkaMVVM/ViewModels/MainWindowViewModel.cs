using AkkaMvvm.Interfaces;
using System.ComponentModel.Composition;

namespace AkkaMvvm.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public ITickerViewModel TickerViewModel { get; }
        public ILogViewModel LogViewModel { get; }

        [ImportingConstructor]
        public MainWindowViewModel(ITickerViewModel tickerViewModel, ILogViewModel logViewModel)
        {
            TickerViewModel = tickerViewModel;
            LogViewModel = logViewModel;
        }
    }
}
