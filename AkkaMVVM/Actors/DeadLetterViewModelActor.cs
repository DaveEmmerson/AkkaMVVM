using System;
using Akka.Actor;
using AkkaMvvm.Interfaces;
using AkkaMvvm.Messages;
using AkkaMvvm.ViewModels;
using Akka.Event;

namespace AkkaMvvm.Actors
{
    public class DeadLetterViewModelActor : ActorViewModelBase, IDeadLetterViewModel
    {
        private string _deadLetters;

        public string DeadLetters
        {
            get { return _deadLetters; }
            private set { Set(ref _deadLetters, value); }
        }
        public DeadLetterViewModelActor(IActorRef parent)
        {
            Receive<DeadLetter>(letter => DeadLetters = letter + Environment.NewLine + DeadLetters);
            parent.Tell(new DeadLetterViewModelCreated(this));
        }
    }
}
