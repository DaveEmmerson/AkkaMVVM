using AkkaMvvm.Interfaces;
using AkkaMvvm.Utilities;

namespace AkkaMvvm.ViewModels
{
    public class LogViewModel : ViewModelBase, ILogViewModel
    {
        private string _text = "";
        public string Text
        {
            get { return _text; }
            set {
                Guard.NotNull(value);
                Set(ref _text, value);
            }
        }
    }
}
