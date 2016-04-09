using Akka.Actor;
using Akka.Event;
using AkkaMvvm.Messages;
using System;

namespace AkkaMvvm.Actors
{
    public class TickerActor : ReceiveActor
    {
        private readonly int Max = 10;
        private readonly double Min = 100.0;
        private readonly double Multiplier = 500.0;

        private IActorRef _log;
        private IActorRef _tickLogger;
        private IActorRef _listener;
        private ICancelable _timerCancellation;
        private int _interval = 1000;

        public TickerActor(IActorRef log)
        {
            _log = log;
            _log.Tell(new Info("ctor", typeof(TickerActor), "Creating new TickerActor"));
            _tickLogger = Context.ActorOf(Props.Create<TickLoggerActor>(_log));
            Stopped();
        }

        private void startTicker(double intervalInMilliseconds)
        {
            _log.Tell(new Debug(nameof(startTicker), typeof(TickerActor), $"Speed is {intervalInMilliseconds}"));
            _timerCancellation.CancelIfNotNull();
            var interval = TimeSpan.FromMilliseconds(intervalInMilliseconds);
            _timerCancellation = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, _tickLogger, new TickMessage(), Self);
        }

        public void Stopped()
        {
            Receive<StartMessage>(message =>
            {
                _log.Tell(new Debug(nameof(Stopped), typeof(TickerActor), "Start message received"));
                Become(Running);
                startTicker(_interval);
                _listener = Context.Sender;
                _listener.Tell(new IsRunningMessage());
            });
        }

        public void Running()
        {
            Receive<StopMessage>(message =>
            {
                _log.Tell(new Debug(nameof(Running), typeof(TickerActor), "Stop message received"));
                Become(Stopped);
                _timerCancellation.Cancel();
                _listener.Tell(new IsStoppedMessage());
            });
            Receive<ChangeSpeedMessage>(message =>
            {
                _log.Tell(new Debug(nameof(Running), typeof(TickerActor), $"Change speed to {message.Speed}"));
                var cappedSpeed = message.Speed > Max ? Max : message.Speed;
                var _interval = Min + (Max - message.Speed) * Multiplier;
                startTicker(_interval);
            });
            Receive<TickMessage>(message => _log.Tell(message));
        }

        public override void AroundPreStart()
        {
            _log.Tell(new Debug(nameof(AroundPreStart), typeof(TickerActor), "In AroundPreStart"));
            base.AroundPreStart();
        }

        public override void AroundPostStop()
        {
            _log.Tell(new Debug(nameof(AroundPostStop), typeof(TickerActor), "In AroundPostStop"));
            _timerCancellation.Cancel();
            _listener.Tell(new IsStoppedMessage());
            base.AroundPostStop();
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            _log.Tell(new Debug(nameof(AroundPreRestart), typeof(TickerActor), "In AroundPreRestart"));
            base.AroundPreRestart(cause, message);
        }

        public override void AroundPostRestart(Exception cause, object message)
        {
            _log.Tell(new Debug(nameof(AroundPostRestart), typeof(TickerActor), "In AroundPostRestart"));
            base.AroundPostRestart(cause, message);
        }
    }
}
