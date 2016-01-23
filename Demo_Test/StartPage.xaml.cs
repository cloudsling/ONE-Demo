using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        public static List<DayObject> DayObjectCollection;

        public static string uri = "http://wufazhuce.com/";

        public static string uriOneObject = "http://wufazhuce.com/one/";

        public static HttpClient httpClient;

        public static string x;

        private async static Task<string> GetOneString(string uri)
        {
            if (httpClient != null) httpClient.Dispose();
            httpClient = new HttpClient();
            x = await httpClient.GetStringAsync(new Uri(uri));
            httpClient.Dispose();
            return x;
        }

        public async static Task<string> GetDayReallyObjectString(string vol)
        {
            if (httpClient != null) httpClient.Dispose();
            httpClient = new HttpClient();
            return await httpClient.GetStringAsync(new Uri(uriOneObject + vol));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{
            //    var bar = StatusBar.GetForCurrentView();
            //    await bar.HideAsync();
            //    StartAnimation.Begin();
            //    var what = bar.ProgressIndicator;
            //    what.ProgressValue = 0;
            //    what.Text = "Many-多个";
            //}
            //必须要做的事情
            GaoStatusBar.HideStatusBar();
            StartAnimation.Begin();
            try
            {
                await GetOneString(uri);
                DayObjectCollection = GetOne.GetOneTodayObjectList(x);
                //GetOne.SavePicHere(DayObjectCollection[0].DayImagePath,"");
            }
            catch (Exception)
            {
                await new MessageDialog("oops！程序出了一点异常，可能是网络连接的问题......").ShowAsync();
                Frame.Navigate(typeof(Main), "404");
                return;
            }
            this.Frame.Navigate(typeof(StartMainPage), new MyNavigationEventArgs(x, DayObjectCollection));
        }
    }

    public class MyNavigationEventArgs
    {
        public MyNavigationEventArgs(string xx, List<DayObject> dayObjectCollection)
        {
            XX = xx;
            this.dayObjectCollection = dayObjectCollection;
        }
        public string XX { get; set; }

        public List<DayObject> dayObjectCollection { get; set; }
    }

}
