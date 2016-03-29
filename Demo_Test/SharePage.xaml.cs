using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharePage : Page
    {
        public SharePage()
        {
            this.InitializeComponent();
        }

        public DayObject ShareObject { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ShareObject = e.Parameter as DayObject;
            }
            CheckTokenValid();
        }

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public bool IsAuthorized { get; set; }

        async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAuthorized || SdkData.AccessToken == null)
            {
                await GetAuthorizeCode();
                await PostMessage();
            }
            else
            {
               await PostMessage();
            }
        }

        async Task PostMessage()
        {
            using (HttpClient client = new HttpClient())
            {
                Dictionary<string, string> dic = new Dictionary<string, string> {
                    {"access_token" , SdkData.AccessToken },
                    {"status" , ShareObject.MainString }
                };
                HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);

                HttpResponseMessage response = await client.PostAsync(new Uri("https://api.weibo.com/2/statuses/update.json"), content);

                if (response.IsSuccessStatusCode)
                {
                    await new MessageDialog("Success!").ShowAsync();
                    Main.NotifyUserMethod("发表成功！！", 200);
                }
            }
        }

        private async Task GetAuthorizeCode()
        {
            string oauthUrl = string.Format("https://api.weibo.com/oauth2/authorize?client_id={0}&response_type=code&redirect_uri={1}", SdkData.AppKey, SdkData.RedirectUri);

            Uri startUri = new Uri(oauthUrl);
            Uri endUri = new Uri(SdkData.RedirectUri);

            var authenResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);

            switch (authenResult.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    {
                        string authorize_code = string.Empty;
                        var data = authenResult.ResponseData;

                        authorize_code = GetQueryParameter(data, "code");

                        if (string.IsNullOrEmpty(authorize_code) == false)
                        {
                            await Authorize(authorize_code);
                        }
                    }
                    break;
                case WebAuthenticationStatus.UserCancel:
                    break;
                case WebAuthenticationStatus.ErrorHttp:
                    break;
                default:
                    break;
            }
        }

        private async Task Authorize(string authorize_code)
        {
            using (HttpClient client = new HttpClient())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("client_id", SdkData.AppKey);
                dic.Add("client_secret", SdkData.AppSecret);
                dic.Add("grant_type", "authorization_code");
                dic.Add("redirect_uri", SdkData.RedirectUri);
                dic.Add("code", authorize_code);

                HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);

                HttpResponseMessage response = await client.PostAsync(new Uri("https://api.weibo.com/oauth2/access_token"), content);

                response.EnsureSuccessStatusCode();
                string responseBodyAsText = await response.Content.ReadAsStringAsync();

                SdkAuth2Res oauthResult = SerializeOAuthResult<SdkAuth2Res>(responseBodyAsText);

                localSettings.Values["access_token"] = oauthResult.AccessToken;
                localSettings.Values["expires_in"] = long.Parse(oauthResult.ExpriesIn);
                localSettings.Values["last_oauth_time"] = Unix2DateTime.GetUnixTimestamp();
                if (oauthResult.RefreshToken != null)
                {
                    localSettings.Values["refresh_token"] = oauthResult.RefreshToken;
                }
            }
        }

        private void CheckTokenValid()
        {
            try
            {
                if (localSettings.Values.ContainsKey("access_token") == false)
                {
                    IsAuthorized = false;
                }
                else
                {
                    if (localSettings.Values.ContainsKey("expires_in"))
                    {
                        var expiredTime = (long)localSettings.Values["expires_in"];
                        var lastOAuthTime = (long)localSettings.Values["last_oauth_time"];
                        var nowTime = Unix2DateTime.GetUnixTimestamp();

                        if (nowTime >= lastOAuthTime + expiredTime)
                        {
                            IsAuthorized = false;
                        }
                        else
                        {
                            SdkData.AccessToken = localSettings.Values["access_token"] as string;
                            IsAuthorized = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                IsAuthorized = false;
            }
        }

        private T SerializeOAuthResult<T>(string responseStr) where T : class
        {
            T result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(responseStr)))
            {
                result = ser.ReadObject(ms) as T;
            }
            return result;
        }

        internal static string GetQueryParameter(string input, string parameterName)
        {
            char[] splitChars = new char[] { '&', '?' };
            foreach (string item in input.Split(splitChars))
            {
                var parts = item.Split('=');
                if (parts[0] == parameterName)
                {
                    return parts[1];
                }
            }
            return String.Empty;
        }
    }

    [DataContract]
    public sealed class SdkAuth2Res
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "remind_in")]
        public string RemindIn { get; set; }

        [DataMember(Name = "expires_in")]
        public string ExpriesIn { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "uid")]
        public string Uid { get; set; }
    }

    public sealed class SdkData
    {
        /// <summary>
        /// 授权之后的回调地址.
        /// </summary>
        static public string RedirectUri { get; set; } = "https://api.weibo.com/oauth2/default.html";

        /// <summary>
        /// 微博开放平台申请的Appkey.
        /// </summary>
        static public string AppKey { get; set; } = "3491600141";

        /// <summary>
        /// 微博开放平台申请的AppSecret.
        /// </summary>
        static public string AppSecret { get; set; } = "04169d47029cdf03ff9c1472ba037a99";

        static internal string UserAgent { get; set; }

        private static string _accessToken;
        /// <summary>
        /// 授权之后的AccessToken
        /// </summary>
        internal static string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }
    }
}
