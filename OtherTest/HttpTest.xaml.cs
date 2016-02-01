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
    public sealed partial class HttpTest : Page
    {
        public HttpTest()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            Uri uri = new Uri("http://139.129.116.86:8000/api/comment/praise");
            Dictionary<string, string> dic = new Dictionary<string, string> {
                {"itemid","1250" },
                {"cmtid","178" },
                {"type","question" }
            };
            HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            txtResult.Text = await response.Content.ReadAsStringAsync();
        }
    }
}
