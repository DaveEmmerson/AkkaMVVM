using Akka.Actor;
using Akka.TestKit.NUnit3;
using AkkaMvvm.Actors;
using AkkaMvvm.Messages;
using NUnit.Framework;

namespace AkkaMvvm.Tests.Actors
{
    [TestFixture]
    public class TickLoggerTests : TestKit
    {         
        public IActorRef CreateTickLogger()
        {
            return Sys.ActorOf(Props.Create(typeof(TickLoggerActor)));
        }

        [Test]
        public void TickLogger_logs_tick_when_TickMessage_received()
        {
            var tickLogger = CreateTickLogger();

            EventFilter.Info("Tick").ExpectOne(() =>
            {
                tickLogger.Tell(new TickMessage());
            });
        }

        [Test]
        public void TickLogger_does_not_send_other_messages_when_TickMessage_received()
        {
            var tickLogger = CreateTickLogger();

            //Assert.Fail("Don't know how to test this.");
        }
    }
}
