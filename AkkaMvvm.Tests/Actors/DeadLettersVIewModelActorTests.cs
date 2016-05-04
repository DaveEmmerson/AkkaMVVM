using Akka.Actor;
using Akka.Event;
using Akka.TestKit.NUnit3;
using AkkaMvvm.Actors;
using AkkaMvvm.Messages;
using NUnit.Framework;

namespace AkkaMvvm.Tests.Actors
{
    [TestFixture]
    public class DeadLettersViewModelActorTests : TestKit
    {
        /// <summary>
        /// This tests that the ViewModel (the underlying type of the actor) is send to the parent as a message
        /// It also tests that when the actor receives a DeadLetter it changes the DeadLetters property of the ViewModel
        /// Maybe it should only be doing one of these and the other in another test? How?
        /// </summary>
        [Test]
        public void DeadLettersViewModel_DeadLetters_property_can_be_set_and_retrieved()
        {
            var parent = CreateTestProbe();

            var props = new Props(typeof(DeadLettersViewModelActor), new[] { parent.Ref });
            var actor = ActorOfAsTestActorRef<DeadLettersViewModelActor>(props);

            var viewModel = parent.FishForMessage<DeadLettersViewModelCreatedMessage>(message => true).DeadLettersViewModel;

            actor.Receive(new DeadLetter("Test", parent, actor));

            Assert.NotNull(viewModel);
            Assert.NotNull(viewModel.DeadLetters);
            Assert.Greater(viewModel.DeadLetters.Length, 0);
        }
    }
}
