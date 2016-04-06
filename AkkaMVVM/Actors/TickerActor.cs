using Akka.Actor;
using AkkaMvvm.ViewModels;
using System.Timers;

namespace AkkaMvvm.Actors
{
    public class TickerActor : ReceiveActor
    {
        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            private set
            {
                _tickerViewModel.Set(ref _isRunning, value, () =>
                {
                    _timer.Enabled = value;
                });
            }
        }

        private Timer _timer = new Timer();

        private readonly int Max = 10;
        private readonly double Min = 100.0;
        private readonly double Multiplier = 500.0;

        private readonly TickerViewModel _tickerViewModel;

        public TickerActor(TickerViewModel viewModel, ElapsedEventHandler elapsed)
        {
            _tickerViewModel = viewModel;
            _timer.Elapsed += elapsed;
        }

        public void Stopped()
        {
            Receive<StartMessage>(message =>
            {
                Become(Running);
                IsRunning = true;
            });
        }

        public void Running()
        {
            Receive<StopMessage>(message =>
            {
                Become(Stopped);
                IsRunning = false;
            });
            Receive<ChangeSpeedMessage>(message =>
            {
                var cappedSpeed = message.Speed > Max ? Max : message.Speed;
                var interval = Min + (Max - message.Speed) * Multiplier;
                _timer.Interval = interval;
            });
        }
    }
}
