using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo
{
    public class INotifyPropertyChanged_OnPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            var temp = System.Threading.Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propName));
        }
    }
}
