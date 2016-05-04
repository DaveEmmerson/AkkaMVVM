using AkkaMvvm.Interfaces;
using AkkaMvvm.Utilities;

namespace AkkaMvvm.Messages
{
    public class DeadLettersViewModelCreatedMessage
    {
        public IDeadLettersViewModel DeadLettersViewModel { get; }

        public DeadLettersViewModelCreatedMessage(IDeadLettersViewModel deadLettersViewModel)
        {
            Guard.NotNull(deadLettersViewModel);
            DeadLettersViewModel = deadLettersViewModel;
        }
    }
}