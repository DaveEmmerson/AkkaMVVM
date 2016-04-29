using AkkaMvvm.Interfaces;
using AkkaMvvm.ViewModels;
using FakeItEasy;
using NUnit.Framework;

namespace AkkaMvvm.Tests.ViewModels
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        [Test]
        public void MainWindowViewModel_properties_correctly_reflect_constructor_arguments()
        {

            var tickerViewModel = A.Fake<ITickerViewModel>();
            var logViewModel = A.Fake<ILogViewModel>();
            var deadLettersViewModel = A.Fake<IDeadLettersViewModel>();

            var mainWindowViewModel = new MainWindowViewModel(tickerViewModel, logViewModel, deadLettersViewModel);

            Assert.That(mainWindowViewModel.TickerViewModel == tickerViewModel);
            Assert.That(mainWindowViewModel.LogViewModel == logViewModel);
            Assert.That(mainWindowViewModel.DeadLettersViewModel == deadLettersViewModel);
        }
    }
}
