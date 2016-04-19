using AkkaMvvm.Interfaces;

namespace AkkaMvvm.Messages
{
    public class DeadLetterViewModelCreated
    {
        public IDeadLetterViewModel DeadLetterViewModel { get; }

        public DeadLetterViewModelCreated(IDeadLetterViewModel deadLetterViewModel)
        {
            DeadLetterViewModel = deadLetterViewModel;
        }
    }
}