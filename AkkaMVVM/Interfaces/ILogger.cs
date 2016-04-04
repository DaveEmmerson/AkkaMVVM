using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;

namespace AkkaMvvm.Interfaces
{
    [InheritedExport]
    public interface ILogger
    {
        void Verbose(string message, [CallerMemberName] string originalCaller = null);
        void Error(string message, [CallerMemberName] string originalCaller = null);
    }
}
