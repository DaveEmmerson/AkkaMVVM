using System.ComponentModel.Composition;
using System.Windows.Input;

namespace AkkaMvvm.Interfaces
{
    [InheritedExport]
    public interface ITickerViewModel
    {
        ICommand StartCommand { get; }
        ICommand StopCommand { get; }
        int Speed { get; }
    }
}
