using NUnit.Framework;
using AkkaMvvm.Utilities;
using System;

namespace AkkaMvvm.Tests
{
    [TestFixture]
    public class GuardTests
    {
        [Test]
        public void Guard_NotNull_does_not_throw_if_parameter_is_not_null()
        {
            object parameter = new object();
            Guard.NotNull(parameter);
        }

        [Test]
        public void Guard_NotNull_throws_ArgumentNullException_if_parameter_is_null()
        {
            object parameter = null;
            Assert.Throws<ArgumentNullException>(() => Guard.NotNull(parameter));
        }
    }
}