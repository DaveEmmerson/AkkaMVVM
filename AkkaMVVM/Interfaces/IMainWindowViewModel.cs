using System.ComponentModel.Composition;

namespace AkkaMvvm.Interfaces
{
    [InheritedExport]
    public interface IMainWindowViewModel
    {
        ITickerViewModel TickerViewModel { get; }
        ILogViewModel LogViewModel { get; }
    }
}
