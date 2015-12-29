using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.Storage;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            //staDataContext.OneFontSize = 20;
        }
        
        private void slider_LostFocus(object sender, RoutedEventArgs e)
        {
            var a = DataContext as ApplicationModel;
            a.OneSettings.OneFontSize = (sender as Slider).Value;
            SaveSettings();
        }

        private void SaveSettings()
        {
            ApplicationData.Current.LocalSettings.Values["myOneSettings"] = (object)DataContext;
        }

    }
}
