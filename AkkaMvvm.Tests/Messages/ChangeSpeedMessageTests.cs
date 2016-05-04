using AkkaMvvm.Messages;
using NUnit.Framework;

namespace AkkaMvvm.Tests.Messages
{
    [TestFixture]
    public class ChangeSpeedMessageTests
    {
        [Test]
        public void ChangeSpeedMessage_properties_correctly_reflect_constructor_arguments()
        {
            var speed = 4;

            var message = new ChangeSpeedMessage(speed);

            Assert.AreEqual(speed, message.Speed);
        }
    }
}
