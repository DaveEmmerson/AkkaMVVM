﻿using AkkaMvvm.Interfaces;

namespace AkkaMvvm.Messages
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