using Demo.Models;
using JYAnalyticsUniversal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
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

namespace Demo
{
    public sealed partial class Music : Page
    {
        static readonly string musicId = "http://v3.wufazhuce.com:8000/api/music/idlist/0?";
        static readonly string musicUri = "http://m.wufazhuce.com/music/";
        static HttpClient client;
    }

    public sealed partial class Music : Page
    {
        static Music MusicCurrent;

        public Music()
        {
            this.InitializeComponent();
            MusicCurrent = this;
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                if (MusicCurrent != null)
                {
                    await Do();
                }
            }
            else
            {
                var music = e.Parameter as MusicModel;
                musicWebView.Navigate(new Uri(musicUri + music.Id.ToString()));
            }
            JYAnalytics.TrackPageStart("music_page");
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            JYAnalytics.TrackPageEnd("music_page");
        }

        private async Task Do()
        {
            string uri = await GetMusicUri();
            musicWebView.Navigate(new Uri(uri));
            bar.IsIndeterminate = false;
            ring.IsActive = false;
        }

        async Task<string> GetMusicUri()
        {
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("UserAgent", "android-async-http/2.0 (http://loopj.com/android-async-http)");
                string response = await client.GetStringAsync(new Uri(musicId));
                JsonObject json;
                if (JsonObject.TryParse(response, out json))
                {
                    JsonArray array = json.GetNamedArray("data");
                    string temp = array[0].GetString();
                    return $"{musicUri}{temp}";
                }
                return $"{musicUri}0";
            }
        }
    }
}
