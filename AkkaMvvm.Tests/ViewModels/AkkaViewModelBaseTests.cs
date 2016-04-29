using AkkaMvvm.ViewModels;
using NUnit.Framework;
using Akka.TestKit.NUnit3;

namespace AkkaMvvm.Tests.ViewModels
{
    [TestFixture]
    public class ActorViewModelBaseTests : TestKit
    {
        private class TestViewModel : ActorViewModelBase
        {
            private int _testProperty;

            public int TestProperty
            {
                get { return _testProperty; }
                set { Set(ref _testProperty, value); }
            }
        }


        [Test]
        public void ActorViewModelBase_fires_PropertyChanged_when_Set_called()
        {
            var testViewModelActor = ActorOfAsTestActorRef<TestViewModel>();
            var testViewModel = testViewModelActor.UnderlyingActor;

            var fired =
                PropertyChanged.IsFiredBy(testViewModel)
                .WhenExecuting(() => testViewModel.TestProperty = 10);

            Assert.IsTrue(fired);
        }

        [Test]
        public void ActorViewModelBase_sets_backing_field_when_Set_called()
        {
            var testViewModel = new TestViewModel();

            var x = 15;

            testViewModel.TestProperty = x;

            Assert.That(testViewModel.TestProperty == x);
        }
    }
}
