using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OtherTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RequestLogIn : Page
    {
        static string uriOffical = "http://sns.whalecloud.com/sina2/oauth?appkey=50ce92a252701539bf0000e0&uid=cf5337fa9339d88a125cee9f9ceada&sdkv=4.3.0150716&entitykey=c14828b5681668cd33da01b5fca56d07&source=umengplus&imei=cf5337fa9339d88a125cee9f9ceada&os=Android&need_encrypt=true";

        public RequestLogIn()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            using (HttpClient client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                //client.DefaultRequestHeaders.Cookie.Add(new HttpCookiePairHeaderValue(""));

                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    {"","" },

                };

                HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);
                var response = await client.PostAsync(new Uri(uriOffical), content);

                tbox.Text += response;
            }
        }
    }
}
