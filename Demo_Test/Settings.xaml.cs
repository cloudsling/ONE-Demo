using Demo.Common;
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
            titleSize.Value = settings.FontSizeTitle;
            fontSize.Value = settings.FontSizeContent;
            lineHeight.Value = settings.LineHeight;
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

        private void Slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            var slider = sender as Slider;
            switch (slider.Name)
            {
                case "titleSize": settings.FontSizeTitle = slider.Value; break;

                case "fontSize": settings.FontSizeContent = slider.Value; break;
                case "lineHeight": settings.LineHeight = slider.Value; break;
            }
        }
    }
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel()
        {
            AppSettings = new OneSettings();
        }
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

        public OneSettings AppSettings { get; set; }

        //double _titleSize;
        //public double TitleSize
        //{
        //    get { return _titleSize; }
        //    set
        //    {
        //        SetProperty(ref _titleSize, value);
        //    }
        //}

        //double _contentSize;
        //public double ContentSize
        //{
        //    get { return _contentSize; }
        //    set
        //    {
        //        SetProperty(ref _contentSize, value);
        //    }
        //}

        //double _lineHeight;
        //public double LineHeight
        //{
        //    get { return _lineHeight; }
        //    set
        //    {
        //        SetProperty(ref _lineHeight, value);
        //    }
        //}
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
            get { return ReadSettings(nameof(SearchCacheMode), true); }
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

        public double FontSizeTitle
        {
            get { return ReadSettings(nameof(FontSizeTitle), 36.0); }
            set
            {
                SaveSettings(nameof(FontSizeTitle), value);
                OnPropertyChanged();
            }
        }

        public double FontSizeContent
        {
            get { return ReadSettings(nameof(FontSizeContent), 28.0); }
            set
            {
                SaveSettings(nameof(FontSizeContent), value);
                OnPropertyChanged();
            }
        }
        public double LineHeight
        {
            get { return ReadSettings(nameof(LineHeight), 42.0); }
            set
            {
                SaveSettings(nameof(LineHeight), value);
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
