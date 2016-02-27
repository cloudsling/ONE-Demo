using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OtherTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyControlsTest : Page
    {
        public MyControlsTest()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://sns.whalecloud.com/sina2/oauth?appkey=50ce92a252701539bf0000e0&uid=cf5337fa9339d88a125cee9f9ceada&sdkv=4.3.0150716&entitykey=c14828b5681668cd33da01b5fca56d07&source=umengplus&imei=cf5337fa9339d88a125cee9f9ceada&os=Android&need_encrypt=true"));
            request.Headers.Add("UserAgent", "Mozilla/5.0 (Linux; Android 4.1.1; Nexus 7 Build/JRO03D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166  Safari/535.19");
            wv.NavigateWithHttpRequestMessage(request);

            await wv.InvokeScriptAsync("evel", new string[] { "" });
        }

        private void wv_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            var response = args.ToString();

        }

        async void wv_ScriptNotify(object sender, NotifyEventArgs e)
        {
            await new MessageDialog(e.Value).ShowAsync();
        }
    }
}
