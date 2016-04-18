using Akka.Actor;
using Akka.Event;
using AkkaMvvm.Interfaces;
using AkkaMvvm.Messages;
using System;

namespace AkkaMvvm.Actors
{
    public class LogActor : ReceiveActor
    {
        private int _logCount = 0;

        public LogActor(ILogViewModel viewModel)
        {
            Receive<LogEvent>(e =>
            {
                var thread = e.Thread;

                var threadType = thread.IsThreadPoolThread ? "pool" : "UI  ";

                viewModel.Text =
                    $"{e.Timestamp}: {_logCount++:0000} {e.LogClass?.Name}: " +
                    $"{threadType} ({thread.ManagedThreadId:000}): {e.LogSource}: {e.Message}" +
                    Environment.NewLine + viewModel.Text;
            });

            var logRouter = Context.ActorSelection("/system/*-LogRouter");

            logRouter.Tell(new RegisterLoggerMessage());
        }
    }
}