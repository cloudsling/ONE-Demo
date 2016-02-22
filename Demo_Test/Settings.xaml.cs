using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    public sealed partial class Settings : Page
    {
        public SettingsViewModel settingsViewModel { get; set; }

        public static OneSettings settings;
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
            settings = Main.MainCurrent.mainViewModel.oneSettings;
            OneMainPageStyle.SelectedIndex = settings.OneMainPageStyle;
            DoubleClickExit.IsOn = settings.IsDoubleClickExit;
            SunOrNightMode.IsOn = !settings.RequireLightTheme;
            SkipStartMainPage.IsOn = settings.SkipStartMainPage;
            SetLockScreen.IsOn = settings.IsSetLockScreen;
            SearchCache.IsOn = settings.SearchCacheMode;
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

        private async void SetLockScreen_Toggled(object sender, RoutedEventArgs e)
        {
            var temp = sender as ToggleSwitch;
            if (temp.IsOn)
            {
                Main.MainCurrent.mainViewModel.oneSettings.IsSetLockScreen = true;
                //  await SetLockScreenMethod();
                await TaskConfiguration.RegisterBackgroundTask(typeof(BackGround.SetLockScreenTask), "SetLockScreenTask", new TimeTrigger(60 * 6, false), null);
            }
            else {
                Main.MainCurrent.mainViewModel.oneSettings.IsSetLockScreen = false;
                TaskConfiguration.UnregisterBackgroundTasks("SetLockScreenTask");
            }
        }
        static async Task SetLockScreenMethod()
        {
            if (!UserProfilePersonalizationSettings.IsSupported())
            {
                Main.NotifyUserMethod("不受支持", 180);
                return;
            }
            var settings = UserProfilePersonalizationSettings.Current;
            IStorageItem ifile = await ApplicationData.Current.LocalFolder.TryGetItemAsync(Main.DayObjectCollection[0].Vol + ".jpg");
            if (ifile != null)
            {
                var temp = await ApplicationData.Current.LocalFolder.GetFileAsync(Main.DayObjectCollection[0].Vol + ".jpg");
                bool b = await settings.TrySetLockScreenImageAsync(temp); return;
            }
            IStorageItem icachefile = await ApplicationData.Current.LocalCacheFolder.TryGetItemAsync(Main.DayObjectCollection[0].Vol + ".jpg");
            if (icachefile != null)
            {
                StorageFile file = await ApplicationData.Current.LocalCacheFolder.GetFileAsync(Main.DayObjectCollection[0].Vol + ".jpg");
                var temp = await file.CopyAsync(ApplicationData.Current.LocalFolder);
                bool b = await settings.TrySetLockScreenImageAsync(temp);
            }
        }

        private void SearchCache_Toggled(object sender, RoutedEventArgs e)
        {
            var temp = sender as ToggleSwitch;
            if (temp.IsOn)
            {
                settings.SearchCacheMode = true;
            }
            else
            {
                settings.SearchCacheMode = false;
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


        public bool SearchCacheMode
        {
            get { return ReadSettings(nameof(SearchCacheMode), false); }
            set
            {
                SaveSettings(nameof(SearchCacheMode), value);
                OnPropertyChanged();
            }
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
        public bool IsSetLockScreen
        {
            get { return ReadSettings(nameof(IsSetLockScreen), false); }
            set
            {
                SaveSettings(nameof(IsSetLockScreen), value);
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
