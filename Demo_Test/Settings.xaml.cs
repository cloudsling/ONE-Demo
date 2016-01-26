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
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Story.Begin();
            DoubleClickExit.IsOn = Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit;
            OneMainPageStyle.SelectedIndex = Main.MainCurrent.mainViewModel.oneSettings.OneMainPageStyle;
            SunOrNightMode.IsOn = !Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme;
        }

        async void HyperlinkButton_Click(object sender, RoutedEventArgs e) =>
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));


        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!DoubleClickExit.IsOn)
            {
                Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit = false;
            }
            else
            {
                Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit = true;
            }
        }

        void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            Main.MainCurrent.mainViewModel.oneSettings.OneMainPageStyle = OneMainPageStyle.SelectedIndex;


        void SunOrNightMode_Toggled(object sender, RoutedEventArgs e)
        {
            if (SunOrNightMode.IsOn)
            {
                Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme = false;
                ThemeColorModel.InitialByOtherObject(Main.MainCurrent.mainViewModel.themeColorModelSettings, ThemeColorModel.NightModeTheme);
                Main.MainCurrent.ChangeSunOrNightMode(false);
            }
            else
            {
                Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme = true;
                ThemeColorModel.InitialByOtherObject(Main.MainCurrent.mainViewModel.themeColorModelSettings, ThemeColorModel.SunModeTheme);
                Main.MainCurrent.ChangeSunOrNightMode(true);
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

        T ReadSettings<T>(string key, T defaultValue)
        {
            if (LocalSettings.Values.ContainsKey(key))
            {
                return (T)LocalSettings.Values[key];
            }
            if (defaultValue != null)
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

        public int OneMainPageStyle
        {
            get { return ReadSettings(nameof(OneMainPageStyle), 0); }

            set
            {
                SaveSettings(nameof(OneMainPageStyle), value);
                OnPropertyChanged();
            }
        }

        public int GiveMeGood
        {
            get { return ReadSettings(nameof(GiveMeGood), 0); }
            set
            {
                SaveSettings(nameof(GiveMeGood), value);
                OnPropertyChanged();
            }
        }

        public bool RequireLightTheme
        {
            get { return ReadSettings(nameof(RequireLightTheme), true); }
            set
            {
                SaveSettings(nameof(RequireLightTheme), value);
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
