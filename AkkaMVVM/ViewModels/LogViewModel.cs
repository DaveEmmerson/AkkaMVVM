using AkkaMvvm.Interfaces;
using Akka.Event;
using System;

namespace AkkaMvvm.ViewModels
{
    public class LogViewModel : ViewModelBase, ILogViewModel
    {
        private string _text = "";
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }
    }
}
