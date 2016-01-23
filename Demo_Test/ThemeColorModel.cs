using Windows.UI.Xaml.Media;
using Windows.UI;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Demo
{
    public class ThemeColorModel :INotifyPropertyChanged
    {
        public double Opacity { get; set; }

        public SolidColorBrush StatusBarBackGroundColor { get; set; }

        public SolidColorBrush TitleBarBackgroundColor { get; set; }

        public SolidColorBrush MainBodyBackgroundColor { get; set; }

        public SolidColorBrush AppBarBackGroundColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propName));
        }
    }
}
