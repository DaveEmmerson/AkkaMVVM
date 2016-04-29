using AkkaMvvm.Interfaces;

namespace AkkaMvvm.Messages
{
    public class DeadLettersViewModelCreated
    {
        public IDeadLettersViewModel DeadLettersViewModel { get; }

        public DeadLettersViewModelCreated(IDeadLettersViewModel deadLettersViewModel)
        {
            DeadLettersViewModel = deadLettersViewModel;
        }
    }
}