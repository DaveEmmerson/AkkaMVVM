using Akka.Actor;
using Akka.Event;
using AkkaMvvm.Messages;

namespace AkkaMvvm.Actors
{
    public class TickLoggerActor : ReceiveActor
    {
        public TickLoggerActor(IActorRef _log)
        {
            Receive<TickMessage>(_ =>
            {
                _log.Tell(new Info("TickLoggerActor", null, "Tick"));
            });
        }
    }
}
