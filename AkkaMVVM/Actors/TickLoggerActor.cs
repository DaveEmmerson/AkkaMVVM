using Akka.Actor;
using Akka.Event;
using AkkaMvvm.Messages;

namespace AkkaMvvm.Actors
{
    public class TickLoggerActor : ReceiveActor
    {
        public TickLoggerActor()
        {
            var log = Context.GetLogger();
            Receive<TickMessage>(_ =>
            {
                log.Info("Tick");
            });
        }
    }
}
