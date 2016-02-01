using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace Demo
{
    public sealed partial class Serial : Page
    {
        public Serial()
        {
            this.InitializeComponent();
        }
        public static string html;

      
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Size size = Window.Current.Content.DesiredSize;

            serial.Height = size.Height;
            serial.Width = size.Width;
            // string temp = await GetOneString(html);
            serial.Navigate(new Uri(html));
            //  serial.NavigateToString(temp);
            //serial.Navigate(new Uri(Main.dayReallyObject.SerialAddress));
        }
    }
}
