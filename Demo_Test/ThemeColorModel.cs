using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Demo
{
    class ThemeColorModel
    {
        public ThemeColorModel()
        {
            TitleBarBackgroundColor = new SolidColorBrush(Color.FromArgb(0xFF, 45, 41, 39));
        }
        public SolidColorBrush TitleBarBackgroundColor { get; set; }
    }
}
