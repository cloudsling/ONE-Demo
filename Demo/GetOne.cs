using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;

namespace Demo
{
    public class DayObject : INotifyPropertyChanged
    {
        private string _vol;

        private string _headerString;

        private string _mainString;

        private string _byWho;

        private string _dayImagePath;

        private string _oneDay;

        private string _oneMonthAndYear;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Vol
        {
            get
            {
                return _vol;
            }

            set
            {
                _vol = value;
                OnPropertyChanged();
            }
        }

        public string HeaderString
        {
            get
            {
                return _headerString;
            }

            set
            {
                _headerString = value;
                PropertyChangedEventHandler temp = Volatile.Read(ref PropertyChanged);
                if (temp != null) temp(this, new PropertyChangedEventArgs("HeaderString"));
            }
        }

        public string MainString
        {
            get
            {
                return _mainString;
            }

            set
            {
                _mainString = value;
                OnPropertyChanged();
            }
        }

        public string ByWho
        {
            get
            {
                return _byWho;
            }

            set
            {
                _byWho = value;
                OnPropertyChanged();
            }
        }

        public string DayImagePath
        {
            get
            {
                return _dayImagePath;
            }

            set
            {
                _dayImagePath = value;
                OnPropertyChanged();
            }
        }

        public string OneDay
        {
            get
            {
                return _oneDay;
            }

            set
            {
                _oneDay = value;
                OnPropertyChanged();
            }
        }

        public string OneMonthAndYear
        {
            get
            {
                return _oneMonthAndYear;
            }

            set
            {
                _oneMonthAndYear = value;
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DayObjectBinding : ObservableCollection<DayObject>
    {
        public DayObjectBinding()
        {
            var collection = GetOne.GetOneTodayObjectList(Main.x);
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }
        public DayObjectBinding(int index)
        {
            var collection = GetOne.GetOneTodayObjectList(Main.x);
            this.Add(collection[index]);
        }
    }
    /// <summary>
    /// 核心---每天的总对象
    /// </summary>
    public class DayReallyObject
    {
        private List<DayObject> _oneMain;
        private ArticlesObject _articles;
        private OneQuestionObject _oneQuestion;
        private OneThingObject _oneThing;

        public ArticlesObject Articles
        {
            get
            {
                return _articles;
            }

            set
            {
                _articles = value;
            }
        }

        public OneQuestionObject OneQuestion
        {
            get
            {
                return _oneQuestion;
            }

            set
            {
                _oneQuestion = value;
            }
        }

        public List<DayObject> OneMain
        {
            get
            {
                return _oneMain;
            }

            set
            {
                _oneMain = value;
            }
        }

        public OneThingObject OneThing
        {
            get
            {
                return _oneThing;
            }

            set
            {
                _oneThing = value;
            }
        }

    }
    /// <summary>
    /// 文章对象
    /// </summary>
    public class ArticlesObject : INotifyPropertyChanged
    {
        private string _headContent;
        private string _writer;
        private string _header;
        private string _content;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public string Header
        {
            get
            {
                return _header;
            }

            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        public string Writer
        {
            get
            {
                return _writer;
            }

            set
            {
                _writer = value;
                OnPropertyChanged();
            }
        }

        public string HeadContent
        {
            get
            {
                return _headContent;
            }

            set
            {
                _headContent = value;
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// ONE问题对象
    /// </summary>
    public class OneQuestionObject : INotifyPropertyChanged
    {
        private string _askerName;
        private string _askContent;
        private string _answerName;
        private string _answerContent;

        public event PropertyChangedEventHandler PropertyChanged;

        public string AnswerContent
        {
            get
            {
                return _answerContent;
            }

            set
            {
                _answerContent = value;
                OnPropertyChanged();
            }
        }

        public string AnswerName
        {
            get
            {
                return _answerName;
            }

            set
            {
                _answerName = value;
                OnPropertyChanged();
            }
        }

        public string AskContent
        {
            get
            {
                return _askContent;
            }

            set
            {
                _askContent = value;
                OnPropertyChanged();
            }
        }

        public string AskerName
        {
            get
            {
                return _askerName;
            }

            set
            {
                _askerName = value;
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// ONE东西对象
    /// </summary>
    public class OneThingObject : INotifyPropertyChanged
    {
        private string _imagePath;
        private string _header;
        private string _content;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string proptyName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(proptyName));
        }

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public string Header
        {
            get
            {
                return _header;
            }

            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        public string ImagePath
        {
            get
            {
                return _imagePath;
            }

            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }
    }
    /// <summary>
    /// 核心类，返回各种各样的对象
    /// </summary>
    public class GetOne
    {
        public static Regex regex;
        public static HttpClient httpClient;
        public static string x;
        public static List<string> imageSourceAsync;
        public static Uri uri = new Uri("http://wufazhuce.com/");
        public List<string> imageSource;
        #region 首页代码
        public static List<DayObject> dayObjectCollection;
        /// <summary>
        /// 得到首页图片
        /// </summary>
        /// <param name="resultString"></param>
        /// <returns></returns>
        public static List<string> GetOneTodayImageSourceAsync(string resultString)
        {
            imageSourceAsync = new List<string>();
            foreach (var item in Main.DayObjectCollection)
            {
                imageSourceAsync.Add(item.DayImagePath);
            }
            return imageSourceAsync;
        }
        /// <summary>
        /// 得到7个对象
        /// </summary>
        /// <param name="resultString"></param>
        /// <returns></returns>
        public static List<DayObject> GetOneTodayObjectList(string resultString)
        {
            CutOneString(ref resultString);
            dayObjectCollection = new List<DayObject>();
            for (int i = 0; i < 7; i++)
            {
                dayObjectCollection.Add(new DayObject());
            }
            GetObjectDayImagePath(resultString, ref dayObjectCollection);
            GetObjectHeaderString(resultString, ref dayObjectCollection);
            GetObjectMainString(resultString, ref dayObjectCollection);
            GetObjectByWho(resultString, ref dayObjectCollection);
            GetObjectVOL(resultString, ref dayObjectCollection);
            GetObjectOneDay(resultString, ref dayObjectCollection);
            GetObjectOneMonthAndYear(resultString, ref dayObjectCollection);
            return dayObjectCollection;
        }
        /// <summary>
        /// 处理获取的字符串，获得对象集合的字符串
        /// </summary>
        /// <param name="resultString"></param>
        private static void CutOneString(ref string resultString)
        {
            regex = new Regex("<div class=\"carousel-inner\">([\\d\\D]+)<a class=\"left carousel-control\" href=\"#carousel-one\" data-slide=\"prev\">", RegexOptions.Multiline);
            var resultCollection = regex.Matches(resultString);
            resultString = resultCollection[0].Groups[1].ToString();
        }
        /// <summary>
        /// 得到对象的ImagePath
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectDayImagePath(string resultString, ref List<DayObject> list)
        {
            regex = new Regex("<img.+src=\"(.+jpg)\".+/>");
            MatchCollection coll = regex.Matches(resultString);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].DayImagePath = coll[i].Groups[1].ToString();
            }
        }
        /// <summary>
        /// 获取对象的HeaderString
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectHeaderString(string resultString, ref List<DayObject> list)
        {
            regex = new Regex("fp-one-imagen-footer\">([\\d\\D]+?)<br");
            var coll = regex.Matches(resultString);
            // Console.WriteLine(s);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].HeaderString = coll[i].Groups[1].ToString().Trim();
            }
        }
        /// <summary>
        /// 获取对象的MainString
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectMainString(string resultString, ref List<DayObject> list)
        {
            regex = new Regex("img[\\d\\D]+?com/one/vol.{5}\">([\\d\\D]+?)</a>");
            var coll = regex.Matches(resultString);
            // Console.WriteLine(s);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].MainString = coll[i].Groups[1].ToString();
            }
        }
        /// <summary>
        /// 得到对象的ByWho
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectByWho(string resultString, ref List<DayObject> list)
        {
            regex = new Regex("fp-one-imagen-footer[\\d\\D]+?<br />([\\d\\D]+?作品).+?</div>");
            var coll = regex.Matches(resultString);
            // Console.WriteLine(s);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].ByWho = coll[i].Groups[1].ToString().Trim();
            }
        }
        /// <summary>
        /// 得到对象的VOL
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectVOL(string resultString, ref List<DayObject> list)
        {
            regex = new Regex(">VOL.(.+?)</p>");
            var coll = regex.Matches(resultString);
            // Console.WriteLine(s);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Vol = "VOL." + coll[i].Groups[1].ToString();
            }
        }
        /// <summary>
        /// 得到对象的OneDay
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectOneDay(string resultString, ref List<DayObject> list)
        {
            regex = new Regex("\">(..)</p>");
            var coll = regex.Matches(resultString);
            // Console.WriteLine(s);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].OneDay = coll[i].Groups[1].ToString();
            }
        }
        /// <summary>
        /// 得到对象的OneMonthAndYear
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectOneMonthAndYear(string resultString, ref List<DayObject> list)
        {
            regex = new Regex("y\">(.{8})</p>");
            var coll = regex.Matches(resultString);
            // Console.WriteLine(s);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].OneMonthAndYear = coll[i].Groups[1].ToString().Replace(' ', ',');
            }
        }

        #endregion

        #region 每天的对象

        private static IBuffer GetBufferFromArrayByte(byte[] buffer)
        {
            using (InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter dataWriter = new DataWriter(memoryStream))
                {
                    dataWriter.WriteBytes(buffer);
                    return dataWriter.DetachBuffer();
                }
            }
        }

        public static async void SavePic(string uri, string fileName)
        {
            // SavePicHere("");
            if (httpClient != null) httpClient.Dispose();
            httpClient = new HttpClient();
            StorageFile file = await Main.oneFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            try
            {
                byte[] bytes = await httpClient.GetByteArrayAsync(uri);
                IBuffer buffer = GetBufferFromArrayByte(bytes);
                await FileIO.WriteBufferAsync(file, buffer);
            }
            catch (Exception)
            {
            }
        }

        public static async void SavePicHere(string uri)
        {
            try
            {
                if (httpClient != null) httpClient.Dispose();
                httpClient = new HttpClient();
                StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync("StartMainPage.jpg",CreationCollisionOption.OpenIfExists);
                byte[] bytes = await httpClient.GetByteArrayAsync(uri);
                IBuffer buffer = GetBufferFromArrayByte(bytes);
                await FileIO.WriteBufferAsync(file, buffer);
            }
            catch (Exception)
            {
            }
        }
        

        public static DayReallyObject dayReallyObject;
        /// <summary>
        /// 得到 真-每日对象
        /// </summary>
        /// <param name="TodayMainString"></param>
        /// <returns></returns>
        public static DayReallyObject GetTodayReallyObject(string TodayMainString)
        {
            //dayReallyObject = new DayReallyObject();
            FormatTodayReallyObject(ref TodayMainString);
            dayReallyObject = new DayReallyObject();
            GetOneQuestionObject(TodayMainString, ref dayReallyObject);
            GetArticlesObject(TodayMainString, ref dayReallyObject);
            GetOneThingObject(TodayMainString, ref dayReallyObject);
            dayReallyObject.OneMain = Main.DayObjectCollection;
            return dayReallyObject;
        }
        /// <summary>
        /// 处理字符串便于构造对象
        /// </summary>
        /// <param name="TodayMainString"></param>
        private static void FormatTodayReallyObject(ref string TodayMainString)
        {
            regex = new Regex("(comilla-cerrar[\\d\\D]+)cosas-compartir");
            var coll = regex.Matches(TodayMainString);
            TodayMainString = coll[0].Groups[1].ToString();
        }
        /// <summary>
        /// 得到对象的ONE-THING对象
        /// </summary>
        /// <param name="ResultString"></param>
        /// <param name="dayReallyObject"></param>
        private static void GetOneThingObject(string ResultString, ref DayReallyObject dayReallyObject)
        {
            MatchCollection coll;
            OneThingObject oneThingObject = new OneThingObject();
            StringBuilder sb;
            //得到header
            regex = new Regex("cosas-titulo..([\\d\\D]+?)</h2>");
            coll = regex.Matches(ResultString);
            string temp = coll[0].Groups[1].ToString().Trim();
            regex = new Regex("<.+?>(.+?)<.+>");
            coll = regex.Matches(temp);
            if (coll.Count > 0) oneThingObject.Header = coll[0].Groups[1].ToString().Trim();
            oneThingObject.Header = temp;
            //得到content
            regex = new Regex("cosas-contenido..([\\d\\D]+?)</div>");
            coll = regex.Matches(ResultString);
            sb = new StringBuilder(coll[0].Groups[1].ToString().Trim());
            oneThingObject.Content = sb.Replace("<br />", "").ToString();
            //得到图片路径
            regex = new Regex("<img.+src=\"(.+jpg)\".+/>");
            coll = regex.Matches(ResultString);
            oneThingObject.ImagePath = coll[0].Groups[1].ToString();

            dayReallyObject.OneThing = oneThingObject;
        }
        /// <summary>
        /// 得到对象的ONE-QUESTION对象
        /// </summary>
        /// <param name="ResultString"></param>
        /// <param name="dayReallyObject"></param>
        private static void GetOneQuestionObject(string ResultString, ref DayReallyObject dayReallyObject)
        {
            MatchCollection coll;
            OneQuestionObject oneThingObject = new OneQuestionObject();
            //得到askerName和得到answerName
            regex = new Regex("h4>([\\d\\D]+?)</h4>");
            coll = regex.Matches(ResultString);
            oneThingObject.AskerName = coll[0].Groups[1].ToString().Trim();
            oneThingObject.AnswerName = coll[1].Groups[1].ToString().Trim();
            //得到askerContent和answerCentent
            regex = new Regex("cuestion-contenido..([\\d\\D]+?)</div>");
            coll = regex.Matches(ResultString);
            oneThingObject.AskContent = coll[0].Groups[1].ToString().Trim();
            StringBuilder sb = new StringBuilder(coll[1].Groups[1].ToString().Trim());
            sb = sb.Replace("\r\n", "").Replace("</p>", "\r\n").Replace("<br />", "").Replace("&nbsp;", "").Replace("&mdash;", "—").Replace("&hellip;", "…").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("<p>", "    ");
            oneThingObject.AnswerContent = sb.ToString();
            dayReallyObject.OneQuestion = oneThingObject;
        }
        /// <summary>
        /// 得到对象的ONE-ARTICLES
        /// </summary>
        /// <param name="ResultString"></param>
        /// <param name="dayReallyObject"></param>
        private static void GetArticlesObject(string ResultString, ref DayReallyObject dayReallyObject)
        {
            MatchCollection coll;
            ArticlesObject articles = new ArticlesObject();
            //得到
            regex = new Regex("comilla-cerrar..([\\d\\D]+?)</div>");
            coll = regex.Matches(ResultString);
            articles.HeadContent = coll[0].Groups[1].ToString().Trim();
            //得到
            regex = new Regex("articulo-titulo..([\\d\\D]+?)</h2>");
            coll = regex.Matches(ResultString);
            articles.Header = coll[0].Groups[1].ToString().Trim();
            //得到
            regex = new Regex("articulo-autor..([\\d\\D]+?)</p>");
            coll = regex.Matches(ResultString);
            articles.Writer = coll[0].Groups[1].ToString().Trim();

            regex = new Regex("articulo-contenido..([\\d\\D]+?)</strong></p>");
            coll = regex.Matches(ResultString);
            StringBuilder sb = new StringBuilder(coll[0].Groups[1].ToString().Trim());
            sb = sb.Replace("\r\n", "").Replace("&nbsp;", " ").Replace("&hellip;", "…").Replace("&mdash;", "—").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("</p>", "\r\n").Replace("<strong>", "   ").Replace("<p>", "   ").Replace("<br />", "\r\n");
            articles.Content = sb.ToString();
            dayReallyObject.Articles = articles;
        }

        #endregion

    }

}
