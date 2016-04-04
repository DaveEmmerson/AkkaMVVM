using System.ComponentModel.Composition;

namespace AkkaMvvm.Interfaces
{
    [InheritedExport]
    public interface ILogViewModel
    {
        string Text { get; }
    }
}
