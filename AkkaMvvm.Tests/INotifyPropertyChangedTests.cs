using System;
using System.ComponentModel;

namespace AkkaMvvm.Tests
{
    public class PropertyChangedTest
    {
        private INotifyPropertyChanged _implementation;

        public PropertyChangedTest(INotifyPropertyChanged implementation)
        {
            _implementation = implementation;
        }

        public bool WhenExecuting(Action action)
        {
            bool fired = false;

            // todo: look at Rx for this when have signal.

            PropertyChangedEventHandler handler = new PropertyChangedEventHandler((sender, eventArgs) =>
            {
                fired = true;
            });

            try
            {
                _implementation.PropertyChanged += handler;

                action();

            }
            finally
            {
                _implementation.PropertyChanged -= handler;
            }

            return fired;
        }
    }

    public static class PropertyChanged
    {
        public static PropertyChangedTest IsFiredBy(INotifyPropertyChanged implementation)
        {
            return new PropertyChangedTest(implementation);
        }
    }
}
