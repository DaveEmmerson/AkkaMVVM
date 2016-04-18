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

        private ILoggingAdapter _log;
        private IActorRef _tickLogger;
        private IActorRef _listener;
        private ICancelable _timerCancellation;
        private int _interval;

        private static int _actors = 1;
        private string _actorName;
            
        public TickerActor()
        {
            _actorName = $"Actor {_actors++}";
            _log = Context.GetLogger();
            _tickLogger = Context.ActorOf<TickLoggerActor>();
            Stopped();
        }

        private void startTicker(double intervalInMilliseconds)
        {
            _timerCancellation.CancelIfNotNull();
            var interval = TimeSpan.FromMilliseconds(intervalInMilliseconds);
            _timerCancellation = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, _tickLogger, new TickMessage(), Self);
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
                Become(Running);
                startTicker(_interval);
            });
            Receive<ChangeSpeedMessage>(message =>
            {
                updateSpeed(message);
            });
        }

        public void Running()
        {
            _listener.Tell(new IsRunningMessage());
            Receive<StopMessage>(message =>
            {
                Become(Stopped);
                _timerCancellation.Cancel();
                _listener.Tell(new IsStoppedMessage());
            });
            Receive<ChangeSpeedMessage>(message =>
            {
                updateSpeed(message);
                startTicker(_interval);
            });
        }

        public override void AroundPostStop()
        {
            _log.Debug(_actorName);
            _timerCancellation.CancelIfNotNull();
            base.AroundPostStop();
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            _listener.Tell(new StopMessage());
            _listener.Tell(new IsStoppedMessage());
            base.AroundPreRestart(cause, message);
        }

        public override void AroundPostRestart(Exception cause, object message)
        {
            _log.Debug($"Exception: {cause.Message}, Message: {message}, {_actorName}");
            base.AroundPostRestart(cause, message);
        }
    }
}
