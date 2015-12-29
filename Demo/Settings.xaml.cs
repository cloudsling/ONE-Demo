using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Story.Begin();
            DoubleClickExit.IsOn = Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit;
        }
        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DoubleClickExit.IsOn == false)
            {
                Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit = false;
            }
            else
            {
                Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit = true;
            }
        }
    }

    public class OneSettings : INotifyPropertyChanged
    {
        public OneSettings()
        {
            LocalSettings = ApplicationData.Current.LocalSettings;
        }

        public ApplicationDataContainer LocalSettings { get; set; }

        public void SaveSettings(string key, object value)
        {
            LocalSettings.Values[key] = value;
        }

        public T ReadSettings<T>(string key, T defaultValue)
        {
            if (LocalSettings.Values.ContainsKey(key))
            {
                return (T)LocalSettings.Values[key];
            }
            if (null != defaultValue)
            {
                return defaultValue;
            }
            return default(T);
        }

        public bool IsDoubleClickExit
        {
            get { return ReadSettings(nameof(IsDoubleClickExit), true); }

            set
            {
                SaveSettings(nameof(IsDoubleClickExit), value);
                OnPropertyChanged();
            }
        }

        public bool Switch
        {
            get { return ReadSettings(nameof(Switch), true); }

            set
            {
                SaveSettings(nameof(Switch), value);
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
