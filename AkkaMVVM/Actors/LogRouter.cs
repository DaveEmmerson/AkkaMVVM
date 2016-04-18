using Akka.Actor;
using Akka.Event;
using AkkaMvvm.Messages;
using System.Collections.Generic;

namespace AkkaMvvm.Actors
{
    public class LogRouter : ReceiveActor
    {
        private readonly List<IActorRef> loggers = new List<IActorRef>();

        public LogRouter()
        {
            Receive<LogEvent>(e =>
            {
                foreach(var logger in loggers)
                {
                    logger.Tell(e);
                }

            });

            Receive<InitializeLogger>(e =>
            {
                Sender.Tell(new LoggerInitialized());
            });

            Receive<RegisterLoggerMessage>(e =>
            {
                loggers.Add(Sender);
            });
        }
    }
}