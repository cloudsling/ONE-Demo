using Demo.Models;
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
            NavigationCacheMode = NavigationCacheMode.Required;
        }
        public static string html;


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var temp = e.Parameter as ReadingModel;
                if (temp != null)
                {
                    serial.Navigate(new Uri("http://m.wufazhuce.com/serial/" + temp.ID.ToString()));
                }
            }
            else
            {
                Do();
            }
        }

        private void Do()
        {
            serial.Navigate(new Uri(html));
        }
    }
}
