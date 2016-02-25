using Demo;
using Demo.Common;
using Demo.http;
using Demo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;
using System.Diagnostics.Contracts;

namespace Demo.ViewModel
{
    /// <summary>
    /// constructor and method
    /// </summary>
    public partial class SearchPageViewModel : BindableBase
    {
        public SearchPageViewModel()
        {
            NoSearchResult = Visibility.Collapsed;
            DayObject = new ObservableCollection<DayObject>();
            ReadingOC = new ObservableCollection<ReadingModel>();
            MusicOC = new ObservableCollection<MusicModel>();
            MovieOC = new ObservableCollection<MovieModel>();
            PeopleOC = new ObservableCollection<PeopleModel>();
            ThemeColor = new ThemeColorModel();
            SearchCommand = new CommandBase(Search);
        }

        async void Search(object sth)
        {
            string temp = sth.ToString();
            switch (temp)
            {
                //插图
                case "1": await SearchMain(); break;
                //文章
                case "2": await SearchArticle(); break;
                //music
                case "3": await SearchMusic(); break;
                //movie
                case "4": await SearchMovie(); break;
                //author
                case "5": await SearchPeople(); break;
            }
        }

        #region SearchPeople

        public async Task SearchPeople()
        {
            Active();
            await ClearAllOC();
            //TODO
            using (HttpClient client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                string response = await client.GetStringAsync(new Uri(string.Format(ServiceUri.onesearchauthor, SearchContent)));
                foreach (var item in GetPeopleObjectList(response))
                {
                    PeopleOC.Add(item);
                    await Task.Delay(1);
                }
            }
            if (PeopleOC.Count == 0)
            {
                NoSearchResult = Visibility.Visible;
            }
            Stop();
        }
        public static IEnumerable<PeopleModel> GetPeopleObjectList(string response)
        {
            JsonObject obj;
            if (JsonObject.TryParse(response, out obj))
            {
                JsonArray array = obj.GetNamedArray("data");
                foreach (var item in array)
                {
                    var temp = item.GetObject();
                    PeopleModel author = new PeopleModel
                    {
                        Desc = TryGetFromJsonObject<string>(temp, "desc"),
                        UserId = TryGetFromJsonObject<string>(temp, "user_id"),
                        UserName = TryGetFromJsonObject<string>(temp, "user_name"),
                        WebUri = TryGetFromJsonObject<string>(temp, "web_url")
                    };
                    yield return author;
                }
            }
        }
        #endregion

        #region SearchMovie
        public async Task SearchMovie()
        {
            Active();
            await ClearAllOC();
            //TODO
            using (HttpClient client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                string response = await client.GetStringAsync(new Uri(string.Format(ServiceUri.onesearchmovie, SearchContent)));
                foreach (var item in GetMovieObjectList(response))
                {
                    MovieOC.Add(item);
                    await Task.Delay(1);
                }
            }
            if (MovieOC.Count == 0)
            {
                NoSearchResult = Visibility.Visible;
            }
            Stop();
        }
        public static IEnumerable<MovieModel> GetMovieObjectList(string response)
        {
            JsonObject obj;
            if (JsonObject.TryParse(response, out obj))
            {
                JsonArray array = obj.GetNamedArray("data");
                foreach (var item in array)
                {
                    var temp = item.GetObject();
                    MovieModel model = new MovieModel
                    {
                        Cover = TryGetFromJsonObject<string>(temp, "cover"),
                        Id = TryGetFromJsonObject<string>(temp, "id"),
                        Sorce = TryGetFromJsonObject<string>(temp, "score"),
                        Title = TryGetFromJsonObject<string>(temp, "title")
                    };
                    yield return model;
                }
            }
        }
        #endregion

        #region ClearOC
        public async Task ClearAllOC()
        {
            await ClearSingleOC(DayObject);
            await ClearSingleOC(ReadingOC);
            await ClearSingleOC(MusicOC);
            await ClearSingleOC(MovieOC);
            await ClearSingleOC(PeopleOC);
        }

        public async Task ClearSingleOC<T>(ObservableCollection<T> oc)
        {
            if (oc != null) oc.Clear();
            await Task.Delay(1);
        }
        #endregion

        #region SearchMusic

        public async Task SearchMusic()
        {
            Active();
            await ClearAllOC();
            //TODO
            using (HttpClient client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                string response = await client.GetStringAsync(new Uri(string.Format(ServiceUri.onesearchmusic, SearchContent)));
                foreach (var item in GetMusicObjectList(response))
                {
                    MusicOC.Add(item);
                    await Task.Delay(1);
                }
            }
            if (MusicOC.Count == 0)
            {
                NoSearchResult = Visibility.Visible;
            }
            Stop();
        }
        public static IEnumerable<MusicModel> GetMusicObjectList(string response)
        {
            JsonObject obj;
            if (JsonObject.TryParse(response, out obj))
            {
                JsonArray array = obj.GetNamedArray("data");
                foreach (var item in array)
                {
                    var temp = item.GetObject();
                    var auth = TryGetFromJsonObject<JsonObject>(temp, "author");
                    PeopleModel author = new PeopleModel
                    {
                        Desc = TryGetFromJsonObject<string>(auth, "desc"),
                        UserId = TryGetFromJsonObject<string>(auth, "user_id"),
                        UserName = TryGetFromJsonObject<string>(auth, "user_name"),
                        WebUri = TryGetFromJsonObject<string>(auth, "web_url")
                    };
                    MusicModel model = new MusicModel
                    {
                        Author = author,
                        Cover = TryGetFromJsonObject<string>(temp, "cover"),
                        Id = TryGetFromJsonObject<string>(temp, "id"),
                        MusicId = TryGetFromJsonObject<string>(temp, "music_id"),
                        Title = TryGetFromJsonObject<string>(temp, "title"),
                        Platform = TryGetFromJsonObject<string>(temp, "platform")
                    };
                    yield return model;
                }
            }
        }
        #endregion

        #region SearchArticle

        public async Task SearchArticle()
        {
            Contract.Ensures(Contract.Result<Task>() != null);
            Active();
            await ClearAllOC();
            //TODO
            using (HttpClient client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                while (SearchContent == null)
                {
                    await Task.Delay(10);
                }
                string response = await client.GetStringAsync(new Uri(string.Format(ServiceUri.onesearcharticle, SearchContent)));
                foreach (var item in GetOneArticleObjectList(response))
                {
                    ReadingOC.Add(item);
                    await Task.Delay(1);
                }
            }
            if (ReadingOC.Count == 0)
            {
                NoSearchResult = Visibility.Visible;
            }
            Stop();
        }
        public static IEnumerable<ReadingModel> GetOneArticleObjectList(string response)
        {
            JsonObject obj;
            if (JsonObject.TryParse(response, out obj))
            {
                JsonArray array = obj.GetNamedArray("data");
                foreach (var item in array)
                {
                    var temp = item.GetObject();
                    Type t = null;
                    switch (TryGetFromJsonObject<string>(temp, "type"))
                    {
                        case "question": t = typeof(OneQuestion); break;
                        case "essay": t = typeof(Articles); break;
                        case "serialcontent": t = typeof(Serial); break;
                    }
                    var ReadingModel = new ReadingModel
                    {
                        ID = TryGetFromJsonObject<string>(temp, "id"),
                        Title = TryGetFromJsonObject<string>(temp, "title"),
                        PageType = t,
                        Number = TryGetFromJsonObject<string>(temp, "number")
                    };
                    yield return ReadingModel;
                }
            }
        }

        #endregion

        #region SearchMain
        public async Task SearchMain()
        {
            Active();
            await ClearAllOC();
            using (HttpClient client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                while (SearchContent == null)
                {
                    await Task.Delay(10);
                }
                string response = await client.GetStringAsync(new Uri(string.Format(ServiceUri.onesearchmain, SearchContent)));
                foreach (var item in GetOneTodayObjectList(response))
                {
                    DayObject.Add(item);
                    await Task.Delay(1);
                }
            }
            if (DayObject.Count == 0)
            {
                NoSearchResult = Visibility.Visible;
            }
            Stop();
        }

        public static IEnumerable<DayObject> GetOneTodayObjectList(string resultString)
        {
            JsonObject json;
            if (JsonObject.TryParse(resultString, out json))
            {
                JsonArray oneMainContentCollection = json.GetNamedArray("data");
                for (int i = 0; i < oneMainContentCollection.Count; i++)
                {
                    JsonObject item = oneMainContentCollection[i].GetObject();
                    DayObject dayObject = new DayObject();
                    dayObject.DayImagePath = item.GetNamedString("hp_img_original_url") ?? "可能服务器出现故障-_-";
                    dayObject.MainString = item.GetNamedString("hp_content") ?? "可能服务器出现故障-_-";
                    dayObject.ByWho = item.GetNamedString("hp_author") ?? "可能服务器出现故障-_-";
                    dayObject.Vol = item.GetNamedString("hp_title") ?? "可能服务器出现故障-_-";
                    dayObject.ItemId = item.GetNamedString("hpcontent_id") ?? "可能服务器出现故障-_-";
                    dayObject.PraiseName = item.GetNamedNumber("praisenum").ToString() ?? "可能服务器出现故障-_-";
                    string[] temp = item.GetNamedString("hp_makettime")?.Split(' ');
                    if (temp.Length == 0)
                    {
                        dayObject.OneMonthAndYear = "可能服务器出现故障-_-";
                        dayObject.OneDay = "可能服务器出现故障-_-";
                    }
                    dayObject.OneMonthAndYear = temp[0].Substring(0, temp[0].LastIndexOf('-'));
                    dayObject.OneDay = temp[0].Substring(temp[0].LastIndexOf('-') + 1);
                    yield return dayObject;
                }
            }
        }

        #endregion

        static T TryGetFromJsonObject<T>(JsonObject obj, string valueName)
        {
            //Type defTyle = typeof(string);
            JsonValue value = JsonValue.CreateNullValue();
            try
            {
                value = obj.GetNamedValue(valueName);
            }
            catch (Exception)
            {

                throw;
            }
            switch (value.ValueType)
            {
                case JsonValueType.String:
                    return (T)(object)value.GetString();
                case JsonValueType.Null:
                    break;
                case JsonValueType.Boolean:
                    return (T)(object)value.GetBoolean();
                case JsonValueType.Number:
                    return (T)(object)value.GetNumber().ToString();
                case JsonValueType.Array:
                    return (T)(object)value.GetArray();
                case JsonValueType.Object:
                    return (T)(object)value.GetObject();
            }
            return default(T);
        }

        #region UI property method
        void Active()
        {
            IsActive = true;
            Visable = Visibility.Visible;
            NoSearchResult = Visibility.Collapsed;
        }
        void Stop()
        {
            Visable = Visibility.Collapsed;
            IsActive = false;
        }
        #endregion
    }

    /// <summary>
    /// properties and field
    /// </summary>
    public partial class SearchPageViewModel : BindableBase
    {
        public string SearchContent { get; set; }

        public ObservableCollection<DayObject> DayObject { get; set; }

        public ObservableCollection<ReadingModel> ReadingOC { get; set; }

        public ObservableCollection<MusicModel> MusicOC { get; set; }

        public ObservableCollection<MovieModel> MovieOC { get; set; }

        public ObservableCollection<PeopleModel> PeopleOC { get; set; }

        ThemeColorModel _themeColor;
        public ThemeColorModel ThemeColor
        {
            get { return _themeColor; }
            set
            {
                SetProperty(ref _themeColor, value);
            }
        }
        public CommandBase SearchCommand { get; set; }

        bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty(ref _isActive, value);
            }
        }

        Visibility _visable;
        public Visibility Visable
        {
            get { return _visable; }
            set
            {
                SetProperty(ref _visable, value);
            }
        }

        Visibility _noSearchResult;
        public Visibility NoSearchResult
        {
            get { return _noSearchResult; }
            set
            {
                SetProperty(ref _noSearchResult, value);
            }
        }
    }
}
