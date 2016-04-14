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
        private int _interval;

        private static int _actors = 1;
        private string _actorName;
            
        public TickerActor(IActorRef log)
        {
            _actorName = $"Actor {_actors++}";
            _log = log;
            _log.Tell(new Debug("ctor", typeof(TickerActor), $"{_actorName}"));
            _tickLogger = Context.ActorOf(Props.Create<TickLoggerActor>(_log));
            _log.Tell(new Debug("ctor", typeof(TickerActor), $"Becoming Stopped ({_actorName})"));
            Stopped();
        }

        private void startTicker(double intervalInMilliseconds)
        {
            _log.Tell(new Debug(nameof(startTicker), typeof(TickerActor), $"Speed is {intervalInMilliseconds} ({_actorName})"));
            _timerCancellation.CancelIfNotNull();
            var interval = TimeSpan.FromMilliseconds(intervalInMilliseconds);
            _log.Tell(new Debug(nameof(startTicker), typeof(TickerActor), $"Starting schedule ({_actorName})"));
            _timerCancellation = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, _tickLogger, new TickMessage(), Self);
            _log.Tell(new Debug(nameof(startTicker), typeof(TickerActor), $"Schedule started ({_actorName})"));
        }

        private void updateSpeed(ChangeSpeedMessage message)
        {
            var cappedSpeed = message.Speed > Max ? Max : message.Speed;
            _interval = (int)(Min + (Max - message.Speed) * Multiplier);
        }

        public void Stopped()
        {
            Receive<StartMessage>(message =>
            {
                _listener = Sender;
                _log.Tell(new Debug(nameof(Receive), typeof(TickerActor), $"StartMessage {_actorName}"));
                Become(Running);
                startTicker(_interval);
            });
            Receive<ChangeSpeedMessage>(message =>
            {
                _log.Tell(new Debug(nameof(Stopped), typeof(TickerActor), $"Change speed to {message.Speed} ({_actorName})"));
                updateSpeed(message);
            });
        }

        public void Running()
        {
            _listener.Tell(new IsRunningMessage());
            Receive<StopMessage>(message =>
            {
                _log.Tell(new Debug(nameof(Receive), typeof(TickerActor), $"StopMessage {_actorName}"));
                Become(Stopped);
                _timerCancellation.Cancel();
                _listener.Tell(new IsStoppedMessage());
            });
            Receive<ChangeSpeedMessage>(message =>
            {
                _log.Tell(new Debug(nameof(Running), typeof(TickerActor), $"Change speed to {message.Speed} ({_actorName})"));
                updateSpeed(message);
                startTicker(_interval);
            });
            Receive<TickMessage>(message => _log.Tell(message));
        }

        public override void AroundPreStart()
        {
            _log.Tell(new Debug(nameof(AroundPreStart), typeof(TickerActor), _actorName));
            base.AroundPreStart();
        }

        public override void AroundPostStop()
        {
            _log.Tell(new Debug(nameof(AroundPostStop), typeof(TickerActor), _actorName));
            _timerCancellation.CancelIfNotNull();
            base.AroundPostStop();
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            _log.Tell(new Debug(nameof(AroundPreRestart), typeof(TickerActor), $"Exception: {cause.Message}, Message: {message}, {_actorName}"));
            _listener.Tell(new StopMessage());
            _log.Tell(new Debug(nameof(AroundPreRestart), typeof(TickerActor), $"StopMessage sent ({_actorName})"));
            _listener.Tell(new IsStoppedMessage());
            _log.Tell(new Debug(nameof(AroundPreRestart), typeof(TickerActor), $"IsStoppedMessage sent ({_actorName})"));
            base.AroundPreRestart(cause, message);
        }

        public override void AroundPostRestart(Exception cause, object message)
        {
            _log.Tell(new Debug(nameof(AroundPostRestart), typeof(TickerActor), $"Exception: {cause.Message}, Message: {message}, {_actorName}"));
            base.AroundPostRestart(cause, message);
        }
    }
}
