using Akka.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AkkaMvvm.ViewModels
{
    public class ActorViewModelBase : ReceiveActor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ActorViewModelBase() {}

        public void Set<T>(ref T oldValue, T newValue, [Optional] Action ifChangedAction, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                oldValue = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                ifChangedAction?.Invoke();
            }
        }
    }
}
