using Akka.Actor;
using AkkaMvvm.Interfaces;

namespace AkkaMvvm.ViewModels
{
    public class DeadMessagesViewModel : ViewModelBase, IDeadMessagesViewModel
    {

        public string DeadMessages => "Weird.";

        public DeadMessagesViewModel()
        {
        }
    }
}
