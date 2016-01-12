using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

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

        static MatchCollection coll;

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
            GetObjectHeaderString(resultString, ref dayObjectCollection);
            GetObjectMainString(resultString, ref dayObjectCollection);
            GetObjectByWho(resultString, ref dayObjectCollection);
            GetObjectVOL(resultString, ref dayObjectCollection);
            GetObjectOneDay(resultString, ref dayObjectCollection);
            GetObjectOneMonthAndYear(resultString, ref dayObjectCollection);
            GetObjectDayImagePath(resultString, ref dayObjectCollection);
            return dayObjectCollection;
        }
        /// <summary>
        /// 处理获取的字符串，获得对象集合的字符串
        /// </summary>
        /// <param name="resultString"></param>
        private static void CutOneString(ref string resultString)
        {
            try
            {
                regex = new Regex("<div class=\"carousel-inner\">([\\d\\D]+)<a class=\"left carousel-control\" href=\"#carousel-one\" data-slide=\"prev\">", RegexOptions.Multiline);
                coll = regex.Matches(resultString);
                resultString = coll[0].Groups[1].ToString();
            }
            catch (Exception)
            {
            }
        }
        private static MatchCollection GetAccurateStringList(string regexString, string resultString)
        {
            regex = new Regex(regexString);
            return regex.Matches(resultString);
        }
        /// <summary>
        /// 得到对象的ImagePath
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectDayImagePath(string resultString, ref List<DayObject> list)
        {
            string temp;
            coll = GetAccurateStringList("<img.+src=\"(.+?)\".+/>", resultString);
            for (int i = 0; i < list.Count; i++)
            {
                temp = coll[i].Groups[1].ToString();
                //list[i].DayImagePath
                SavePicHere(temp, list[i].Vol + ".jpg", list[i]);
                //StorageFile file = await Main.oneFolder.GetFileAsync(list[i].Vol + ".jpg");
                // list[i].DayImagePath = TempPath;
            }
            try
            {

            }
            catch (Exception)
            {
            }
        }

        public static string TempPath = "";

        /// <summary>
        /// 获取对象的HeaderString
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectHeaderString(string resultString, ref List<DayObject> list)
        {
            try
            {
                coll = GetAccurateStringList("fp-one-imagen-footer\">([\\d\\D]+?)<br", resultString);
                // Console.WriteLine(s);
                for (int i = 0; i < list.Count; i++)
                {
                    StringBuilder sb = new StringBuilder(coll[i].Groups[1].ToString().Trim());
                    list[i].HeaderString = sb.Replace("&#039;", "\"").ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 获取对象的MainString
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectMainString(string resultString, ref List<DayObject> list)
        {
            try
            {
                coll = GetAccurateStringList("img[\\d\\D]+?com/one/vol.{5}\">([\\d\\D]+?)</a>", resultString);
                // Console.WriteLine(s);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].MainString = coll[i].Groups[1].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 得到对象的ByWho
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectByWho(string resultString, ref List<DayObject> list)
        {
            try
            {
                coll = GetAccurateStringList("fp-one-imagen-footer[\\d\\D]+?<br />([\\d\\D]+?作品).+?</div>", resultString);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ByWho = coll[i].Groups[1].ToString().Trim();
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 得到对象的VOL
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectVOL(string resultString, ref List<DayObject> list)
        {
            try
            {
                coll = GetAccurateStringList(">VOL.(.+?)</p>", resultString);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Vol = "VOL." + coll[i].Groups[1].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 得到对象的OneDay
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectOneDay(string resultString, ref List<DayObject> list)
        {
            try
            {
                coll = GetAccurateStringList("\">(.{1,2})</p>", resultString);
                for (int i = 0; i < list.Count; i++)
                {
                    StringBuilder sb = new StringBuilder(coll[i].Groups[1].ToString());
                    if (sb.Length == 2)
                    {
                        list[i].OneDay = coll[i].Groups[1].ToString();
                    }
                    else
                    {
                        list[i].OneDay = "0" + coll[i].Groups[1].ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 得到对象的OneMonthAndYear
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        private static void GetObjectOneMonthAndYear(string resultString, ref List<DayObject> list)
        {
            coll = GetAccurateStringList("y\">(.{8})</p>", resultString);
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
            try
            {
                StorageFile target = await ApplicationData.Current.LocalCacheFolder.GetFileAsync(uri);
                StorageFile file = await Main.oneFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                IBuffer buffer = await FileIO.ReadBufferAsync(target);
                await FileIO.WriteBufferAsync(file, buffer);
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// 保存到临时文件中
        /// </summary>
        /// <param name="uri"></param>
        public static async void SavePicHere(string uri, string fileName, DayObject obj)
        {
            if (httpClient != null) httpClient.Dispose();
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            obj.DayImagePath = file.Path;
            try
            {
                httpClient = new HttpClient();
                byte[] bytes = await httpClient.GetByteArrayAsync(uri);
                IBuffer buffer = GetBufferFromArrayByte(bytes);
                await FileIO.WriteBufferAsync(file, buffer);
            }
            catch (Exception)
            {
                SavePicHere(uri, fileName, obj);
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
            TodayMainString = GetAccurateString("(comilla-cerrar[\\d\\D]+)cosas-compartir", TodayMainString);
        }
        /// <summary>
        /// 得到对象的ONE-THING对象
        /// </summary>
        /// <param name="ResultString"></param>
        /// <param name="dayReallyObject"></param>
        private static void GetOneThingObject(string ResultString, ref DayReallyObject dayReallyObject)
        {
            OneThingObject oneThingObject = new OneThingObject();

            //得到header
            //regex = new Regex("cosas-titulo..([\\d\\D]+?)</h2>");
            //coll = regex.Matches(ResultString);
            string temp = GetAccurateString("cosas-titulo..([\\d\\D]+?)</h2>", ResultString);
            regex = new Regex("<.+?>(.+?)<");
            MatchCollection coll = regex.Matches(temp);
            if (coll.Count > 0)
            {
                oneThingObject.Header = coll[0].Groups[1].ToString().Trim();
            }
            else
            {
                oneThingObject.Header = temp;
            }
            //得到content
            //regex = new Regex("cosas-contenido..([\\d\\D]+?)</div>");
            //coll = regex.Matches(ResultString);
            StringBuilder sb = new StringBuilder(GetAccurateString("cosas-contenido..([\\d\\D]+?)</div>", ResultString));
            oneThingObject.Content = sb.Replace("<br />", "").ToString();
            //得到图片路径
            //regex = new Regex("<img.+src=\"(.+jpg)\".+/>");
            //coll = regex.Matches(ResultString);
            oneThingObject.ImagePath = GetAccurateString("<img.+src=\"(.+jpg)\".+/>", ResultString);

            dayReallyObject.OneThing = oneThingObject;
        }
        /// <summary>
        /// 得到对象的ONE-QUESTION对象
        /// </summary>
        /// <param name="ResultString"></param>
        /// <param name="dayReallyObject"></param>
        private static void GetOneQuestionObject(string ResultString, ref DayReallyObject dayReallyObject)
        {
            OneQuestionObject oneThingObject = new OneQuestionObject();
            oneThingObject.AskerName = GetAccurateString("h4>([\\d\\D]+?)</h4>", ResultString);
            oneThingObject.AnswerName = GetAccurateString("h4>([\\d\\D]+?)</h4>", ResultString, 1);
            oneThingObject.AskContent = GetAccurateString("cuestion-contenido..([\\d\\D]+?)</div>", ResultString);
            StringBuilder sb = new StringBuilder(GetAccurateString("cuestion-contenido..([\\d\\D]+?)</div>", ResultString, 1));
            sb = sb.Replace("\r\n", "").Replace("</p>", "\r\n\r\n").Replace("<br />", "\r\n").Replace("&nbsp;", "").Replace("&mdash;", "—").Replace("&hellip;", "…").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("<p>", "     ");
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
            ArticlesObject articles = new ArticlesObject();
            articles.HeadContent = GetAccurateString("comilla-cerrar..([\\d\\D]+?)</div>", ResultString);
            articles.Header = GetAccurateString("articulo-titulo..([\\d\\D]+?)</h2>", ResultString);
            articles.Writer = GetAccurateString("articulo-autor..([\\d\\D]+?)</p>", ResultString);
            StringBuilder sb = new StringBuilder(GetAccurateString("articulo-contenido..([\\d\\D]+?)<p class=\"articulo-editor", ResultString));
            sb = sb.Replace("\r\n", "").Replace("&nbsp;", " ").Replace("&hellip;", "…").Replace("&mdash;", "—").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("</p>", "\r\n\r\n").Replace("<strong>", "     ").Replace("<p>", "     ").Replace("<br />", "\r\n").Replace("<em>", "").Replace("</em>", "").Replace("</div>", "");
            articles.Content = sb.ToString();
            dayReallyObject.Articles = articles;
        }

        private static string GetAccurateString(string regexString, string resultString, int index = 0)
        {
            regex = new Regex(regexString);
            coll = regex.Matches(resultString); try
            {
                var res = coll[index].Groups[1];
                return res != null ? res.ToString().Trim() : "发生了什么！(⊙ˍ⊙)？";
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
        #endregion

    }

}
