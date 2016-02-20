using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static Demo.LastUpdate;

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

        async static Task<string> GetOneString(string uri)
        {
            if (httpClient != null) httpClient.Dispose();
            httpClient = new HttpClient();
            x = await httpClient.GetStringAsync(new Uri(uri));
            httpClient.Dispose();
            return x;
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            GaoStatusBar.HideStatusBar();
            StartAnimation.Begin();

            try
            {
                await GetOneString(uri);
                await SaveX(x);
                DayObjectCollection = GetOne.GetOneTodayObjectList(x);


            }
            catch (Exception)
            {
                IsFromInternet = false;
                x = await LoadX();
                DayObjectCollection = GetOne.GetOneTodayObjectList(x);
                await Task.Delay(2500);
            }

            if (!new OneSettings().SkipStartMainPage)
                Frame.Navigate(typeof(StartMainPage), new MyNavigationEventArgs(x, DayObjectCollection));
            else
            {
                Frame.Navigate(typeof(Main), new MyNavigationEventArgs(x, DayObjectCollection));
            }
        }

        public static bool IsFromInternet = true;
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
