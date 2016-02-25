using Demo.http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.ApplicationModel;
using JYAnalyticsUniversal;
using Demo.Common;

namespace Demo
{
    public sealed partial class Movie : Page
    {
        public static Movie movieCurrent;
        public MovieViewModel movieViewModel { get; set; }
        public static readonly string movieListUri = "http://v3.wufazhuce.com:8000/api/movie/list/0?";
    }
    public sealed partial class Movie : Page
    {
        public Movie()
        {
            movieViewModel = new MovieViewModel();
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            movieCurrent = this;
        }

        private void movieList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var click = e.ClickedItem as MovieListModel;
            if (click != null)
            {
                //TODO:
                this.Frame.Navigate(typeof(MovieDetile), $"http://m.wufazhuce.com/movie/{click.Id}");
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await Do();
            JYAnalytics.TrackPageStart("movie_page");
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            JYAnalytics.TrackPageEnd("movie_page");
        }

        private async Task Do()
        {
            movieCurrent.movieViewModel.MovieList = await GetList();
        }

        private static async Task<ObservableCollection<MovieListModel>> GetList()
        {
            using (var client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                //client.DefaultRequestHeaders.Add("UserAgent", "android-async-http/2.0 (http://loopj.com/android-async-http)");
                //client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
                string response = await client.GetStringAsync(new Uri(movieListUri));
                JsonObject json;
                var movieList = new ObservableCollection<MovieListModel>();
                if (JsonObject.TryParse(response, out json))
                {
                    JsonArray array = json.GetNamedArray("data");
                    foreach (var item in array)
                    {
                        var obj = item.GetObject();
                        //TryGetStringFromJsonObject(obj, "score");
                        movieList.Add(new MovieListModel
                        {
                            Cover = TryGetStringFromJsonObject(obj, "cover"),// obj.GetNamedString("cover") ?? "无",
                            Id = TryGetStringFromJsonObject(obj, "id"),// obj.GetNamedString("id") ?? "无",
                            Title = TryGetStringFromJsonObject(obj, "title") ?? "无",
                            Score = TryGetStringFromJsonObject(obj, "score")
                        });
                    }
                }
                return movieList;
            }
        }

        static string TryGetStringFromJsonObject(JsonObject obj, string valueName)
        {
            JsonValue value = JsonValue.CreateNullValue();
            try
            {
                value = obj.GetNamedValue(valueName);
            }
            catch (Exception)
            {

                throw;
            }
            var type = value.ValueType;
            switch (type)
            {
                case JsonValueType.Null:
                    break;
                case JsonValueType.Boolean:
                    break;
                case JsonValueType.Number:
                    break;
                case JsonValueType.String:
                    return value.GetString();
                case JsonValueType.Array:
                    break;
                case JsonValueType.Object:
                    break;
            }
            return string.Empty;
        }
    }

    public class MovieViewModel : BindableBase
    {
        public MovieViewModel()
        {
            MovieList = new ObservableCollection<MovieListModel>();
#if DEBUG
            if (DesignMode.DesignModeEnabled)
            {
                MovieList.Add(new MovieListModel
                {
                    Cover = "ms-appx:///Assets/About/about.png",
                    Id = "34",
                    Title = "卧虎藏龙2：青冥宝剑",
                    Score = "39"
                });
            }
#endif
        }

        private ObservableCollection<MovieListModel> _movieList;

        public ObservableCollection<MovieListModel> MovieList
        {
            get { return _movieList; }
            set { SetProperty(ref _movieList, value); }
        }

    }
    public class ListBinding : ObservableCollection<MovieListModel> { }

    public class MovieListModel
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _score;

        public string Score
        {
            get { return _score; }
            set { _score = value; }
        }
        private string _cover;

        public string Cover
        {
            get { return _cover; }
            set { _cover = value; }
        }

    }
}
