using System;
using System.ComponentModel;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }
    }


    public class OneSettings : INotifyPropertyChanged
    {
        private double _oneFontSize;

        public double OneFontSize
        {
            get
            {
                return _oneFontSize;
            }

            set
            {
                _oneFontSize = value;
                PropertyChangedEventHandler temp = Volatile.Read(ref PropertyChanged);
                if (temp != null) temp(this, new PropertyChangedEventArgs("OneFontSize"));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }


}
