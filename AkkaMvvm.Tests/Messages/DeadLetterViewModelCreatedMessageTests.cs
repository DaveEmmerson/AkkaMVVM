using AkkaMvvm.Interfaces;
using AkkaMvvm.Messages;
using AkkaMvvm.Tests.Messages;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace AkkaMvvm.Tests.Messages
{
    [TestFixture]
    public class DeadLetterViewModelCreatedMessageTests
    {
        [Test]
        public void DeadLetterViewModelCreatedMessage_constructor_throws_ArgumentNullException_if_tickerViewModel_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new DeadLettersViewModelCreatedMessage(null));
        }

        [Test]
        public void DeadLetterViewModelCreatedMessage_properties_correctly_reflect_constructor_arguments()
        {
            var viewModel = A.Fake<IDeadLettersViewModel>();

            var message = new DeadLettersViewModelCreatedMessage(viewModel);

            Assert.AreEqual(viewModel, message.DeadLettersViewModel);
        }
    }
}