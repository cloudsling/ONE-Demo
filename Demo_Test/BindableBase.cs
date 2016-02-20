namespace Demo
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
