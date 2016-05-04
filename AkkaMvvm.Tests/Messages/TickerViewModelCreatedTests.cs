using AkkaMvvm.Interfaces;
using AkkaMvvm.Messages;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace AkkaMvvm.Tests.Messages
{
    [TestFixture]
    public class TickerViewModelCreatedTests
    {
        [Test]
        public void TickerViewModelCreated_constructor_throws_ArgumentNullException_if_tickerViewModel_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new TickerViewModelCreated(null));
        }

        [Test]
        public void TickerViewModelCreated_properties_correctly_reflect_constructor_arguments()
        {
            var viewModel = A.Fake<ITickerViewModel>();

            var message = new TickerViewModelCreated(viewModel);

            Assert.AreEqual(viewModel, message.TickerViewModel);
        }
    }
}
