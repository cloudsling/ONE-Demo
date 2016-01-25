using Windows.UI.Xaml.Media;
using Windows.UI;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Demo
{
    public class ThemeColorModel : INotifyPropertyChanged
    {
        public static readonly ThemeColorModel SunModeTheme = new ThemeColorModel(true);
        public static readonly ThemeColorModel NightModeTheme = new ThemeColorModel(false);

        public static void InitialByOtherObject(ThemeColorModel initialThisObject, ThemeColorModel byThisObject)
        {
            initialThisObject.AppBarBackGroundColor = byThisObject.AppBarBackGroundColor;
            initialThisObject.FontColor = byThisObject.FontColor;
            initialThisObject.MainBodyBackgroundColor = byThisObject.MainBodyBackgroundColor;
            initialThisObject.OneMainBodyBackgroundColor = byThisObject.OneMainBodyBackgroundColor;
            initialThisObject.Opacity = byThisObject.Opacity;
            initialThisObject.SplitViewPanelBackGroundColor = byThisObject.SplitViewPanelBackGroundColor;
            initialThisObject.StatusBarBackGroundColor = byThisObject.StatusBarBackGroundColor;
            initialThisObject.TitleBarBackgroundColor = byThisObject.TitleBarBackgroundColor;
        }
        public ThemeColorModel(bool isLightTheme)
        {
            if (isLightTheme)
            {
                Opacity = 1.0;
                StatusBarBackGroundColor = new SolidColorBrush(ColorHelper.FromArgb(0xFF, 54, 195, 241));
                TitleBarBackgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 230, 230, 230));
                MainBodyBackgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 255, 255, 255));
                OneMainBodyBackgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 255, 255, 255));
                AppBarBackGroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 230, 230, 230));
                SplitViewPanelBackGroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 211, 211, 211));
                FontColor = new SolidColorBrush(Color.FromArgb(0xFF, 0, 0, 0));
            }
            else
            {
                Opacity = 0.2;
                FontColor = new SolidColorBrush(Color.FromArgb(0xFF, 255, 255, 255));
                StatusBarBackGroundColor = new SolidColorBrush(ColorHelper.FromArgb(0xFF, 27, 27, 27));
                TitleBarBackgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 37, 37, 38));
                MainBodyBackgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 45, 45, 48));
                OneMainBodyBackgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 63, 63, 70));
                AppBarBackGroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 37, 37, 38));
                SplitViewPanelBackGroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 112, 112, 112));
            }
        }
        public static ThemeColorModel GetTheme(bool isLightTheme)
        {
            if (isLightTheme) return new ThemeColorModel(true);
            return new ThemeColorModel(false);
        }

        double _opacity;
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OnPropertyChanged();
            }
        }
        SolidColorBrush _fontColor;
        public SolidColorBrush FontColor
        {
            get { return _fontColor; }
            set
            {
                _fontColor = value;
                OnPropertyChanged();
            }
        }
        SolidColorBrush _oneMainBodyBackgroundColor;
        public SolidColorBrush OneMainBodyBackgroundColor
        {
            get { return _oneMainBodyBackgroundColor; }

            set
            {
                _oneMainBodyBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        SolidColorBrush _statusBarBackGroundColor;
        public SolidColorBrush StatusBarBackGroundColor
        {
            get { return _statusBarBackGroundColor; }
            set
            {
                _statusBarBackGroundColor = value;
                OnPropertyChanged();
            }
        }
        SolidColorBrush _titleBarBackgroundColor;
        public SolidColorBrush TitleBarBackgroundColor
        {
            get { return _titleBarBackgroundColor; }
            set
            {
                _titleBarBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        SolidColorBrush _mainBodyBackgroundColor;
        public SolidColorBrush MainBodyBackgroundColor
        {
            get { return _mainBodyBackgroundColor; }
            set
            {
                _mainBodyBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        SolidColorBrush _appBarBackGroundColor;
        public SolidColorBrush AppBarBackGroundColor
        {
            get { return _appBarBackGroundColor; }

            set
            {
                _appBarBackGroundColor = value;
                OnPropertyChanged();
            }
        }
        SolidColorBrush _splitViewPanelBackGroundColor;
        public SolidColorBrush SplitViewPanelBackGroundColor
        {
            get { return _splitViewPanelBackGroundColor; }

            set
            {
                _splitViewPanelBackGroundColor = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propName));
        }
    }
}
