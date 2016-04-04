using AkkaMvvm.Interfaces;
using Akka.Event;
using System;

namespace AkkaMvvm.ViewModels
{
    public partial class LogViewModel : ViewModelBase, ILogViewModel
    {
        #region Properties

        private string _text = "";
        public string Text
        {
            get { return _text; }
            private set { Set(ref _text, value); }
        }

        #endregion 
    }

    public partial class LogViewModel : ILoggingAdapter
    {
        public bool IsDebugEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsWarningEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Debug(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception cause, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warning(string format, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
