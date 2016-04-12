using System.Windows.Input;

namespace AkkaMvvm.Interfaces
{
    public interface ITickerViewModel
    {
        ICommand StartCommand { get; }
        ICommand StopCommand { get; }
        int Speed { get; }
        bool Running { get; set; }
    }
}
