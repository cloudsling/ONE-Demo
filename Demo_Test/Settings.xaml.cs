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
        public SettingsViewModel settingsViewModel { get; set; }

        public Settings()
        {
            settingsViewModel = new SettingsViewModel();
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            settingsViewModel.SettingsThemeColorModel = ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme);
            Story.Begin();
            ReadSettingsToShow();
        }

        void ReadSettingsToShow()
        {
            OneMainPageStyle.SelectedIndex = Main.MainCurrent.mainViewModel.oneSettings.OneMainPageStyle;
            DoubleClickExit.IsOn = Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit;
            SunOrNightMode.IsOn = !Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme;
            SkipStartMainPage.IsOn = Main.MainCurrent.mainViewModel.oneSettings.SkipStartMainPage;
        }

        void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!DoubleClickExit.IsOn) Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit = false;
            else {
                Main.MainCurrent.mainViewModel.oneSettings.IsDoubleClickExit = true;
            }
        }

        void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            Main.MainCurrent.mainViewModel.oneSettings.OneMainPageStyle = OneMainPageStyle.SelectedIndex;

        void SunOrNightMode_Toggled(object sender, RoutedEventArgs e)
        {
            if (SunOrNightMode.IsOn) Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme = false;
            else {
                Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme = true;
            }
            ThemeColorModel.InitialByOtherObject(settingsViewModel.SettingsThemeColorModel, ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme));
            Main.MainCurrent.ThisPageDoWhenThemeChanged();
        }

        void SkipStartMainPage_Toggled(object sender, RoutedEventArgs e)
        {
            if (SkipStartMainPage.IsOn)
                Main.MainCurrent.mainViewModel.oneSettings.SkipStartMainPage = true;
            else {
                Main.MainCurrent.mainViewModel.oneSettings.SkipStartMainPage = false;
            }
        }
    }

    public class SettingsViewModel
    {
        ThemeColorModel _settingsThemeColorModel;

        public ThemeColorModel SettingsThemeColorModel
        {
            get
            {
                return _settingsThemeColorModel;
            }

            set
            {
                _settingsThemeColorModel = value;
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

        public bool SkipStartMainPage
        {
            get { return ReadSettings(nameof(SkipStartMainPage), false); }
            set
            {
                SaveSettings(nameof(SkipStartMainPage), value);
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
