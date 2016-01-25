﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo
{
    public abstract class INotifyPropertyChanged_OnPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            var temp = System.Threading.Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propName));
        }
    }
}