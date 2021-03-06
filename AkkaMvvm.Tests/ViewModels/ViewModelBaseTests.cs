﻿using AkkaMvvm.ViewModels;
using NUnit.Framework;

namespace AkkaMvvm.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        private class TestViewModel : ViewModelBase
        {
            private int _testProperty;

            public int TestProperty
            {
                get { return _testProperty; }
                set { Set(ref _testProperty, value); }
            }            
        }


        [Test]
        public void ViewModelBase_fires_PropertyChanged_when_Set_called()
        {
            var testViewModel = new TestViewModel();

            var fired =
                PropertyChanged.IsFiredBy(testViewModel)
                .WhenExecuting(() => testViewModel.TestProperty = 10);

            Assert.IsTrue(fired);
        }

        [Test]
        public void ViewModelBase_sets_backing_field_when_Set_called()
        {
            var testViewModel = new TestViewModel();

            var x = 15;

            testViewModel.TestProperty = x;

            Assert.That(testViewModel.TestProperty == x);
        }
    }
}
