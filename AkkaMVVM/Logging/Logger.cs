using AkkaMvvm.Interfaces;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace AkkaMvvm.Logging
{
    public class Logger : ILogger
    {
        #region Nested Classes

        private class SeverityAttribute : Attribute
        {
        }

        #endregion

        #region Fields

        public static readonly int maxSeverityNameLength;

        #endregion Fields

        #region Constructors

        public Logger() : this(null) { }

        public Logger([Optional] TaskScheduler scheduler)
        {
        }

        static Logger()
        {
            var type = typeof(Logger);
            var severityMethodLengths =
                from method in type.GetMethods()
                where method.GetCustomAttributes(typeof(SeverityAttribute), inherit: false).Any()
                select method.Name.Length;

            maxSeverityNameLength = severityMethodLengths.Max();
        }

        #endregion Constructors

        #region Methods

        [Severity]
        public void Verbose(string message, [CallerMemberName] string originalCaller = null)
        {

            Write(message, originalCaller);

        }

        [Severity]
        public void Error(string message, [CallerMemberName] string originalCaller = null)
        {

            Write(message, originalCaller);

        }

        private void Write(string message, string originalCaller, [CallerMemberName] string type = null)
        {
            var thread = Thread.CurrentThread;

            var threadType = thread.IsThreadPoolThread ? "pool" : "UI  ";

            /*_agent.Post(
                $"{DateTime.Now}: {_logCount++:0000} {type?.PadRight(maxSeverityNameLength)}: " +
                $"{threadType} ({thread.ManagedThreadId:000}): {originalCaller}: {message}");*/
        }

        #endregion Methods
    }
}
