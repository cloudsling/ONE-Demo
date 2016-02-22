using Demo;
using Demo.Common;
using Demo.http;
using System;
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

namespace Demo.ViewModel
{
    /// <summary>
    /// constructor and method
    /// </summary>
    public partial class SearchPageViewModel : BindableBase
    {
        public SearchPageViewModel()
        {
            DayObject = new ObservableCollection<DayObject>();
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
            }
        }

        public async Task SearchMain()
        {
            IsActive = true;
            Visable = Visibility.Visible;
            if (DayObject.Count > 0)
            {
                DayObject.Clear();
            }
            using (HttpClient client = HttpHelper.CreateHttpClientWithUserAgent())
            {
                while (SearchContent == null)
                {
                    await Task.Delay(50);
                }
                string response = await client.GetStringAsync(new Uri(string.Format(ServiceUri.onesearchmain, SearchContent)));
                foreach (var item in GetOneTodayObjectList(response))
                {
                    DayObject.Add(item);
                }
            }
            Visable = Visibility.Collapsed;
            IsActive = false;
        }

        public static IEnumerable<DayObject> GetOneTodayObjectList(string resultString)
        {

            //CutOneString(ref resultString);
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
                    dayObject.HeaderString = "无题";
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
                    //dayObject.DayImagePath = TempPath;
                }
            }
        }
    }

    /// <summary>
    /// properties and field
    /// </summary>
    public partial class SearchPageViewModel : BindableBase
    {
        public string SearchContent { get; set; }

        public ObservableCollection<DayObject> DayObject { get; set; }

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
    }
}
