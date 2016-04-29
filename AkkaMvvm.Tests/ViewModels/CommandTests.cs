using NUnit.Framework;
using AkkaMvvm.ViewModels;
using System;

namespace AkkaMvvm.Tests.ViewModels
{
    [TestFixture]
    public class CommandTests
    {
        private Action<Action> _dispatcherInvoke = (action) => action();

        [Test]
        public void Commmand_constructor_throws_ArgumentNullException_when_execute_is_null()
        {
            Assert.Throws<ArgumentNullException>( () => new Command((_) => true, null, _dispatcherInvoke));
        }

        [Test]
        public void Command_constructor_throws_ArgumentNullException_when_canExecute_is_null()
        {
            Assert.Throws<ArgumentNullException>( () => new Command(null, _ => {}, _dispatcherInvoke));
        }

        [Test]
        public void Command_calls_canExecute_backing_field_when_CanExecute_called()
        {
            var called = false;

            Func<object, bool> canExecute = _ =>
             {
                 called = true;
                 return true;
             };

            var command = new Command(canExecute, _ => { }, _dispatcherInvoke);

            command.CanExecute(new object());

            Assert.IsTrue(called);
        }

        [Test]
        public void Command_calls_execute_backing_field_when_Execute_called()
        {
            var called = false;

            Action<object> execute = _ =>
            {
                called = true;
            };

            var command = new Command(_ => true, execute, _dispatcherInvoke);

            command.Execute(new object());

            Assert.IsTrue(called);
        }

        [Test]
        public void Command_CanExecuteChanged_raised_when_RaiseCanExecuteChanged_is_called()
        {
            var called = false;

            var command = new Command(_ => true, _ => { }, _dispatcherInvoke);

            EventHandler handler = new EventHandler((sender, args) =>
            {
                called = true;
            });


            command.CanExecuteChanged += handler;

            try
            {
                command.RaiseCanExecuteChanged();

            } finally
            {
                command.CanExecuteChanged -= handler;
            }

            Assert.IsTrue(called);

        }
    }
}
