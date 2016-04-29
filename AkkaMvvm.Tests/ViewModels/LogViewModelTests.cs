using AkkaMvvm.ViewModels;
using NUnit.Framework;
using System;

namespace AkkaMvvm.Tests.ViewModels
{
    [TestFixture]
    public class LogViewModelTests
    {
        [Test]
        public void LogViewModel_Text_property_is_empty_when_constructed()
        {
            var logViewModel = new LogViewModel();

            Assert.IsEmpty(logViewModel.Text);
        }

        [Test]
        public void LogViewModel_Text_property_sets_and_gets()
        {
            var logViewModel = new LogViewModel();

            var x = "Hiya";
            logViewModel.Text = x;

            Assert.That(logViewModel.Text == x);
        }

        [Test]
        public void LogViewModel_Text_property_throws_exception_if_set_to_null()
        {
            var logViewModel = new LogViewModel();

            Assert.Throws<ArgumentNullException>(() => logViewModel.Text = null);
        }
    }
}
