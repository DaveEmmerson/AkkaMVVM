using AkkaMvvm.Interfaces;

namespace AkkaMvvm.Actors
{
    public class TickerViewModelCreated
    {
        public ITickerViewModel TickerViewModel { get; }

        public TickerViewModelCreated(ITickerViewModel tickerViewModel)
        {
            TickerViewModel = tickerViewModel;
        }
    }
}