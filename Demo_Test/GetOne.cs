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
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using Demo.http;
using Newtonsoft.Json;
using static Demo.LastUpdate;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

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

        private string _praiseNum;

        public string PraiseName
        {
            get { return _praiseNum; }
            set
            {
                _praiseNum = value;
                OnPropertyChanged();
            }
        }
        private string _itemId;

        public string ItemId
        {
            get { return _itemId; }
            set
            {
                _itemId = value;
                OnPropertyChanged();
            }
        }

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
        public DayReallyObject()
        {
            _oneQuestion = new OneQuestionObject();
            _articles = new ArticlesObject();
        }
        private List<DayObject> _oneMain;
        private ArticlesObject _articles;
        private OneQuestionObject _oneQuestion;
        // private OneThingObject _oneThing;

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

        public static explicit operator string(DayReallyObject v)
        {
            throw new NotImplementedException();
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
        string _imagePath;
        string _header;
        string _content;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string proptyName = "")
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
        const string TileTempleXml = @"
<tile>
  <visual>

    <binding template='TileMedium' branding='name' displayName='{0}'>
      <text hint-wrap='true' hint-maxLines='2'>{1}</text>
      <text hint-style='captionsubtle' hint-wrap='true'>{2}</text>
    </binding>

    <binding template='TileWide' branding='nameAndLogo' displayName='{0}'>
      <text>{1}</text>
      <text hint-style='captionsubtle' hint-wrap='true'>{2}</text>
      <text hint-style='captionsubtle'>{0}</text>
    </binding>

  </visual>
</tile>"
;
        const string BackgroundContent = "<tile>"
                          + "<visual version='2'>"
                          + "<binding template='TileWide310x150SmallImageAndText03' fallback='TileWideSmallImageAndText03'>"
                          + "<text id='1'>{0}</text>"
                          + "</binding>"
                          + "</visual>"
                          + "</tile>";

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
            //CutOneString(ref resultString);
            dayObjectCollection = new List<DayObject>();
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
                    SavePicHere(dayObject.DayImagePath, dayObject.Vol + ".jpg");
                    var folder = ApplicationData.Current.LocalCacheFolder;
                    dayObject.DayImagePath = $"{folder.Path}\\{dayObject.Vol }.jpg";
                    //dayObject.DayImagePath = TempPath;
                    dayObjectCollection.Add(dayObject);
                }
            }

            //GetObjectHeaderString(resultString, ref dayObjectCollection);
            //GetObjectMainString(resultString, ref dayObjectCollection);
            //GetObjectByWho(resultString, ref dayObjectCollection);
            //GetObjectVOL(resultString, ref dayObjectCollection);
            //GetObjectOneDay(resultString, ref dayObjectCollection);
            //GetObjectOneMonthAndYear(resultString, ref dayObjectCollection);
            //GetObjectDayImagePath(resultString, ref dayObjectCollection);
            UpdateLockScreen(dayObjectCollection, BackgroundContent);
            UpdatePromaryTile(dayObjectCollection);
            return dayObjectCollection;
        }

        static void UpdateLockScreen(List<DayObject> collection, string backgroundContent)
        {
            try
            {
                string xml = string.Format(backgroundContent, collection[0].MainString);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml, new XmlLoadSettings
                {
                    ProhibitDtd = false,
                    ValidateOnParse = false,
                    ElementContentWhiteSpace = false,
                    ResolveExternals = false
                });
                TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(doc));
            }
            catch (Exception)
            {

                throw;
            }
        }

        static void UpdatePromaryTile(List<DayObject> collection)
        {
            try
            {
                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.EnableNotificationQueueForWide310x150(true);
                updater.EnableNotificationQueueForSquare150x150(true);
                updater.Clear();

                foreach (var item in collection)
                {
                    var xml = string.Format(TileTempleXml, item.OneDay, item.Vol, item.MainString);
                    var doc = new XmlDocument();
                    doc.LoadXml(xml, new XmlLoadSettings
                    {
                        ProhibitDtd = false,
                        ValidateOnParse = false,
                        ElementContentWhiteSpace = false,
                        ResolveExternals = false
                    });
                    updater.Update(new TileNotification(doc));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 处理获取的字符串，获得对象集合的字符串
        /// </summary>
        /// <param name="resultString"></param>
        static void CutOneString(ref string resultString)
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
        static MatchCollection GetAccurateStringList(string regexString, string resultString)
        {
            regex = new Regex(regexString);
            return regex.Matches(resultString);
        }

        /// <summary>
        /// 得到对象的ImagePath
        /// </summary>
        /// <param name="resultString"></param>
        /// <param name="list"></param>
        //static void GetObjectDayImagePath(string resultString, ref List<DayObject> list)
        //{
        //    string temp;
        //    coll = GetAccurateStringList("<img.+src=\"(.+?)\".+/>", resultString);
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        temp = coll[i].Groups[1].ToString();
        //        SavePicHere(temp, list[i].Vol + ".jpg", list[i]);
        //    }
        //}

        //public static string TempPath { get; set; } = string.Empty;

        ///// <summary>
        ///// 获取对象的HeaderString
        ///// </summary>
        ///// <param name="resultString"></param>
        ///// <param name="list"></param>
        //static void GetObjectHeaderString(string resultString, ref List<DayObject> list)
        //{
        //    try
        //    {
        //        //coll = GetAccurateStringList("fp-one-imagen-footer\">([\\d\\D]+?)</div", resultString);
        //        // Console.WriteLine(s);
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            //StringBuilder sb = new StringBuilder(coll[i].Groups[1].ToString().Trim());
        //            //list[i].HeaderString = sb.Replace("&#039;", "\"").Replace("&amp;", "&").ToString();
        //            list[i].HeaderString = "无题";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        ///// <summary>
        ///// 获取对象的MainString
        ///// </summary>
        ///// <param name="resultString"></param>
        ///// <param name="list"></param>
        //static void GetObjectMainString(string resultString, ref List<DayObject> list)
        //{
        //    try
        //    {
        //        //coll = GetAccurateStringList("<img[\\d\\D]+?com/one/vol.{5}\">([\\d\\D]+?)</a>", resultString);
        //        coll = GetAccurateStringList("com/one/.{4}\">([\\d\\D]+?)</a>", resultString);
        //        // Console.WriteLine(s);
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            StringBuilder sb = new StringBuilder(coll[2 * i + 1].Groups[1].ToString().Trim());
        //            list[i].MainString = sb.Replace("&#039;", "\"").Replace("&amp;", "&").ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        ///// <summary>
        ///// 得到对象的ByWho
        ///// </summary>
        ///// <param name="resultString"></param>
        ///// <param name="list"></param>
        //static void GetObjectByWho(string resultString, ref List<DayObject> list)
        //{
        //    try
        //    {
        //        //coll = GetAccurateStringList("fp-one-imagen-footer[\\d\\D]+?<br />([\\d\\D]+?作品).+?</div>", resultString);
        //        coll = GetAccurateStringList("fp-one-imagen-footer\">([\\d\\D]+?作品).+?</div>", resultString);
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            StringBuilder sb = new StringBuilder(coll[i].Groups[1].ToString().Trim());
        //            list[i].ByWho = sb.Replace("&#039;", "\"").Replace("&amp;", "&").ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        ///// <summary>
        ///// 得到对象的VOL
        ///// </summary>
        ///// <param name="resultString"></param>
        ///// <param name="list"></param>
        //static void GetObjectVOL(string resultString, ref List<DayObject> list)
        //{
        //    try
        //    {
        //        coll = GetAccurateStringList(">VOL.(.+?)</p>", resultString);
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            list[i].Vol = "VOL." + coll[i].Groups[1].ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        ///// <summary>
        ///// 得到对象的OneDay
        ///// </summary>
        ///// <param name="resultString"></param>
        ///// <param name="list"></param>
        //static void GetObjectOneDay(string resultString, ref List<DayObject> list)
        //{
        //    try
        //    {
        //        coll = GetAccurateStringList("\">(.{1,2})</p>", resultString);
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            StringBuilder sb = new StringBuilder(coll[i].Groups[1].ToString());
        //            if (sb.Length == 2)
        //            {
        //                list[i].OneDay = coll[i].Groups[1].ToString();
        //            }
        //            else
        //            {
        //                list[i].OneDay = "0" + coll[i].Groups[1].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        ///// <summary>
        ///// 得到对象的OneMonthAndYear
        ///// </summary>
        ///// <param name="resultString"></param>
        ///// <param name="list"></param>
        //static void GetObjectOneMonthAndYear(string resultString, ref List<DayObject> list)
        //{
        //    coll = GetAccurateStringList("y\">(.{8})</p>", resultString);
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        list[i].OneMonthAndYear = coll[i].Groups[1].ToString().Replace(' ', ',');
        //    }
        //}

        #endregion

        #region 每天的对象

        static IBuffer GetBufferFromArrayByte(byte[] buffer)
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

        public static async Task SavePic(string uri, string fileName)
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
        public static async void SavePicHere(string uri, string fileName)
        {
            // if (httpClient != null) httpClient.Dispose();

            // CreationCollisionOption.OpenIfExists
            //IStorageItem fds = await ApplicationData.Current.LocalCacheFolder.TryGetItemAsync(fileName);
            //if (await ApplicationData.Current.LocalCacheFolder.TryGetItemAsync(fileName) != null)
            //{
            //    var item = await ApplicationData.Current.LocalCacheFolder.GetFileAsync(fileName);
            //    await item.DeleteAsync();
            //}
            if (!StartPage.IsFromInternet)
            {
                return;
            }
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            try
            {
                byte[] bytes;
                using (httpClient = new HttpClient())
                {
                    bytes = await httpClient.GetByteArrayAsync(uri);
                    IBuffer buffer = GetBufferFromArrayByte(bytes);
                    await FileIO.WriteBufferAsync(file, buffer);
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "|" + e.Data.ToString());
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
            JsonObject json;
            dayReallyObject = new DayReallyObject();
            if (JsonObject.TryParse(TodayMainString, out json))
            {
                JsonObject temp = json.GetNamedObject("data");

                //得到文章
                GetArticle(temp.GetNamedArray("essay"), ref dayReallyObject);

                string articleContent = JsonConvert.SerializeObject(dayReallyObject.Articles);
                SaveSth(articleContent, "article.txt");
                //得到问答
                GetQuestion(temp.GetNamedArray("question"), ref dayReallyObject);

                string quesContent = JsonConvert.SerializeObject(dayReallyObject.OneQuestion);
                SaveSth(quesContent, "question.txt");
                //得到连载
                GetSerial(temp.GetNamedArray("serial"));


            }
            // FormatTodayReallyObject(ref TodayMainString);
            //dayReallyObject = new DayReallyObject();
            //GetOneQuestionObject(TodayMainString, ref dayReallyObject);
            //GetArticlesObject(TodayMainString, ref dayReallyObject);
            //   GetOneThingObject(TodayMainString, ref dayReallyObject);

            dayReallyObject.OneMain = Main.DayObjectCollection;
            return dayReallyObject;
        }

        static void GetArticle(JsonArray articleJson, ref DayReallyObject dayReallyObject)
        {

            JsonObject article = articleJson[0].GetObject();
            string temp = article.GetNamedString("content_id");
            using (httpClient = new HttpClient())
            {
                string response = "";

                response = GetOneString("http://wufazhuce.com/article/" + temp);

                GetArticlesObject(response, ref dayReallyObject);
            }
        }

        private static string GetOneString(string uri)
        {
            httpClient = new HttpClient();
            x = httpClient.GetStringAsync(new Uri(uri)).Result;
            httpClient.Dispose();
            return x;
        }

        static void GetQuestion(JsonArray questionJson, ref DayReallyObject dayReallyObject)
        {
            JsonObject question = questionJson[0].GetObject();
            string temp = question.GetNamedString("question_id");
            string response = GetOneString("http://m.wufazhuce.com/question/" + temp);
            GetOneQuestionObject(response, ref dayReallyObject);
        }
        static void GetSerial(JsonArray aserialJson)
        {
            JsonObject serial = aserialJson[0].GetObject();
            string temp = serial.GetNamedString("id");
            Serial.html = "http://m.wufazhuce.com/serial/" + temp;
        }


        /// <summary>
        /// 处理字符串便于构造对象
        /// </summary>
        /// <param name="TodayMainString"></param>
        static void FormatTodayReallyObject(ref string TodayMainString)
        {
            TodayMainString = GetAccurateString("(comilla-cerrar[\\d\\D]+)cosas-compartir", TodayMainString);
        }

        /// <summary>
        /// 得到对象的ONE-QUESTION对象
        /// </summary>
        /// <param name="ResultString"></param>
        /// <param name="dayReallyObject"></param>
        public static void GetOneQuestionObject(string ResultString, ref DayReallyObject dayReallyObject)
        {
            OneQuestionObject oneThingObject = new OneQuestionObject();
            //Regex reg = new Regex("<!--([\\d\\D]+?)-->");
            //var col = reg.Matches(ResultString);
            //ResultString = reg.Replace(ResultString, "");
            oneThingObject.AskerName = GetAccurateString("(<div class=\"text-detail[\\d\\D]+)<div class=\"download", ResultString);
            //oneThingObject.AnswerName = GetAccurateString("h4>([\\d\\D]+?)</h4>", ResultString, 1);
            //oneThingObject.AskContent = GetAccurateString("cuestion-contenido..([\\d\\D]+?)</div>", ResultString);
            //StringBuilder sb = new StringBuilder(GetAccurateString("cuestion-contenido..([\\d\\D]+?)</div>", ResultString, 1));
            ////sb = sb.Replace("\r\n", "").Replace("</p>", "\r\n\r\n").Replace("<br />", "\r\n").Replace("<br>", "\r\n").Replace("&nbsp;", "").Replace("&mdash;", "—").Replace("&hellip;", "…").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("<p>", "     ").Replace("<strong>", "   ").Replace("</strong>", Environment.NewLine);
            //oneThingObject.AnswerContent = sb.ToString();
            dayReallyObject.OneQuestion = oneThingObject;
        }

        public static string GetOneQuestionObject(string ResultString)
        {
            //OneQuestionObject oneThingObject = new OneQuestionObject();
            //Regex reg = new Regex("<!--([\\d\\D]+?)-->");
            //var col = reg.Matches(ResultString);
            //ResultString = reg.Replace(ResultString, "");
            //oneThingObject.AskerName = GetAccurateString("h4>([\\d\\D]+?)</h4>", ResultString);
            //oneThingObject.AnswerName = GetAccurateString("h4>([\\d\\D]+?)</h4>", ResultString, 1);
            //oneThingObject.AskContent = GetAccurateString("cuestion-contenido..([\\d\\D]+?)</div>", ResultString);
            //StringBuilder sb = new StringBuilder(GetAccurateString("cuestion-contenido..([\\d\\D]+?)</div>", ResultString, 1));
            ////sb = sb.Replace("\r\n", "").Replace("</p>", "\r\n\r\n").Replace("<br />", "\r\n").Replace("<br>", "\r\n").Replace("&nbsp;", "").Replace("&mdash;", "—").Replace("&hellip;", "…").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("<p>", "     ");
            //oneThingObject.AnswerContent = sb.ToString();
            return GetAccurateString("(<div class=\"text-detail[\\d\\D]+?)<div class=\"down", ResultString);

             
        }
        public static string GetOneQestionUri()
        {
            return ServiceUri.article;
            //return GetAccurateString("(http://wufazhuce.com/question/.{4})", resultString);
        }
        /// <summary>
        /// 得到对象的ONE-ARTICLES
        /// </summary>
        /// <param name="ResultString"></param>
        /// <param name="dayReallyObject"></param>
        public static void GetArticlesObject(string ResultString, ref DayReallyObject dayReallyObject)
        {
            ArticlesObject articles = new ArticlesObject();
            articles.HeadContent = GetAccurateString("comilla-cerrar..([\\d\\D]+?)</div>", ResultString);
            articles.Header = GetAccurateString("articulo-titulo..([\\d\\D]+?)</h2>", ResultString);
            articles.Writer = GetAccurateString("articulo-autor..([\\d\\D]+?)</p>", ResultString);
            StringBuilder sb = new StringBuilder(GetAccurateString("articulo-contenido..([\\d\\D]+?)<p class=\"articulo-editor", ResultString));
            sb = sb.Replace("\r\n", "").Replace("&nbsp;", " ").Replace("&hellip;", "…").Replace("&mdash;", "—").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("</p>", "\r\n\r\n").Replace("<strong>", "     ").Replace("<p>", "     ").Replace("<br />", "\r\n").Replace("<br>", "\r\n").Replace("<em>", "").Replace("</em>", "").Replace("</div>", "");
            articles.Content = sb.ToString();
            dayReallyObject.Articles = articles;
        }
        public static ArticlesObject GetArticlesObject(string ResultString)
        {
            ArticlesObject articles = new ArticlesObject();
            articles.HeadContent = GetAccurateString("comilla-cerrar..([\\d\\D]+?)</div>", ResultString);
            articles.Header = GetAccurateString("articulo-titulo..([\\d\\D]+?)</h2>", ResultString);
            articles.Writer = GetAccurateString("articulo-autor..([\\d\\D]+?)</p>", ResultString);
            StringBuilder sb = new StringBuilder(GetAccurateString("articulo-contenido..([\\d\\D]+?)<p class=\"articulo-editor", ResultString));
            sb = sb.Replace("\r\n", "").Replace("&nbsp;", " ").Replace("&hellip;", "…").Replace("&mdash;", "—").Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&rsquo;", "'").Replace("</p>", "\r\n\r\n").Replace("<strong>", "     ").Replace("<p>", "     ").Replace("<br />", "\r\n").Replace("<br>", "\r\n").Replace("<em>", "").Replace("</em>", "").Replace("</div>", "");
            articles.Content = sb.ToString();
            return articles;
        }

        private static string GetAccurateString(string regexString, string resultString, int index = 0)
        {
            regex = new Regex(regexString);
            coll = regex.Matches(resultString);
            try
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
