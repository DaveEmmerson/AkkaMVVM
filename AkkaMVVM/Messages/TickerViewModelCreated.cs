using AkkaMvvm.Interfaces;
using AkkaMvvm.Utilities;

namespace AkkaMvvm.Messages
{
    public class TickerViewModelCreated
    {
        public ITickerViewModel TickerViewModel { get; }

        public TickerViewModelCreated(ITickerViewModel tickerViewModel)
        {
            Guard.NotNull(tickerViewModel);
            TickerViewModel = tickerViewModel;
        }
    }
}