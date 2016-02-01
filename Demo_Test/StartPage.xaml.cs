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

        public static string uri = "http://139.129.116.86:8000/api/hp/more/0?";
        public static string uriOld = "http://wufazhuce.com/";

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
      

        //public async static Task<string> GetDayReallyObjectString(string vol)
        //{
        //    if (httpClient != null) httpClient.Dispose();
        //    httpClient = new HttpClient();
        //    return await httpClient.GetStringAsync(new Uri(uriOneObject + vol));
        //}

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            GaoStatusBar.HideStatusBar();
            StartAnimation.Begin();
            try
            {
                Task task = await Task.Factory.StartNew(async () =>
                {
                    await GetOneString(uri);
                    DayObjectCollection = GetOne.GetOneTodayObjectList(x);
                }, TaskCreationOptions.DenyChildAttach);
                Task.WaitAll(task);
            }
            catch (Exception)
            {
                await new MessageDialog("oops！程序出了一点异常，可能是网络连接的问题......").ShowAsync();
                Frame.Navigate(typeof(Main), "404");
                return;
            }
            if (!new OneSettings().SkipStartMainPage)
                Frame.Navigate(typeof(StartMainPage), new MyNavigationEventArgs(x, DayObjectCollection));
            else
            {
                Frame.Navigate(typeof(Main), new MyNavigationEventArgs(x, DayObjectCollection));
            }
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
