
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.UI.Notifications;

using Windows.Web.Http;

namespace Demo.BackGround
{
    public sealed class LiveTileTask : IBackgroundTask
    {
        static string uri = "http://139.129.116.86:8000/api/hp/more/0?";


        const string TileTempleXml = @"
<tile>
  <visual version='2'>

    <binding template='TileMedium' branding='name' displayName='{0}'>
      <text hint-wrap='true' hint-maxLines='2'>{1}</text>
      <text hint-style='captionsubtle' hint-wrap='true'>{2}</text>
    </binding>

    <binding template='TileWide' branding='nameAndLogo' displayName='{0}'>
      <text>{1}</text>
      <text hint-style='captionsubtle' hint-wrap='true'>{2}</text>
    </binding>
  </visual>
</tile>"
;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
#if DEBUG
            Debug.WriteLine("Background " + taskInstance.Task.Name + " Starting...");
#endif
            var deferral = taskInstance.GetDeferral();
            //TODO something
            await GetLastContent();
            deferral.Complete();
        }

        IAsyncOperation<string> GetLastContent()
        {
            try
            {
                return AsyncInfo.Run(token => GetNews());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetNews()
        {
            using (HttpClient client = new HttpClient())
            {
                string response;
                List<DayObject> collection;
                try
                {
                    response = await client.GetStringAsync(new Uri(uri));
                    collection = GetOneTodayObjectList(response);
                }
                catch (Exception)
                {
                    throw;
                }
                //await UpdateLockScreenImage(collection);
                UpdatePromaryTile(collection);
            }
            return null;
        }

        void UpdatePromaryTile(List<DayObject> collection)
        {
            try
            {
                TrySetLockScreenNotification(collection[0].Vol, collection[0].MainString);
                // NewMethod(collection[0].Vol, collection[0].MainString);
            }
            catch (Exception)
            {

                throw;
            }
            try
            {
                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                //  updater.EnableNotificationQueue(true);
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
                    var a = new ToastNotification(doc);

                    updater.Update(new TileNotification(doc));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TrySetLockScreenNotification(string vol, string content)
        {
            string tileXmlString = "<toast>"
                                  + "<visual version='2'>"
                                  + "<binding template='ToastGeneric'>"
                                  + "<text hint-wrap='true'>{0}</text>"
                                  + "<text hint-wrap='true'>{1}</text>"
                                  + "</binding>"
                                  + "</visual>"
                                  + "</toast>";
            XmlDocument doc = new XmlDocument();
            string xml = string.Format(tileXmlString, vol, content);
            doc.LoadXml(xml, new XmlLoadSettings
            {
                ProhibitDtd = false,
                ValidateOnParse = false,
                ElementContentWhiteSpace = false,
                ResolveExternals = false
            });
            //BadgeNotification badge = new BadgeNotification(doc);
            //a.Update(badge);
            ToastNotification toastNotificate = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toastNotificate);

        }

        private static void NewMethod(string vol, string content)
        {
            var a = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            XmlDocument b = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);
            string ass = b.GetXml();
            a.Clear();
            string tileXmlString = "<badge value='{0}'>"
                              + "</badge>";
            XmlDocument doc = new XmlDocument();
            string xml = string.Format(tileXmlString, content);
            doc.LoadXml(xml, new XmlLoadSettings
            {
                ProhibitDtd = false,
                ValidateOnParse = false,
                ElementContentWhiteSpace = false,
                ResolveExternals = false
            });

            BadgeNotification badge = new BadgeNotification(doc);
            a.Update(badge);
            //TileNotification notificate = new TileNotification(doc);
            //TileUpdateManager.CreateTileUpdaterForApplication().Update(notificate);
        }

        static List<DayObject> GetOneTodayObjectList(string resultString)
        {
            //CutOneString(ref resultString);
            JsonObject json;
            List<DayObject> dayObjectCollection = new List<DayObject>();
            if (JsonObject.TryParse(resultString, out json))
            {
                JsonArray oneMainContentCollection = json.GetNamedArray("data");
                for (int i = 0; i < oneMainContentCollection.Count; i++)
                {
                    if (i > 4) break;
                    JsonObject item = oneMainContentCollection[i].GetObject();
                    DayObject dayObject = new DayObject();
                    dayObject.DayImagePath = item.GetNamedString("hp_img_original_url") ?? "可能服务器出现故障-_-";
                    dayObject.MainString = item.GetNamedString("hp_content") ?? "可能服务器出现故障-_-";
                    //  dayObject.ByWho = item.GetNamedString("hp_author") ?? "可能服务器出现故障-_-";
                    dayObject.Vol = item.GetNamedString("hp_title") ?? "可能服务器出现故障-_-";
                    // dayObject.HeaderString = "无题";
                    string[] temp = item.GetNamedString("hp_makettime")?.Split(' ');
                    if (temp.Length == 0)
                    {
                        dayObject.OneDay = "可能服务器出现故障-_-";
                    }
                    // dayObject.OneMonthAndYear = temp[0].Substring(0, temp[0].LastIndexOf('-'));
                    dayObject.OneDay = temp[0];//.Substring(temp[0].LastIndexOf('-') + 1);
                                               //SavePicHere(dayObject.DayImagePath, dayObject.Vol + ".jpg");
                                               //var folder = ApplicationData.Current.LocalCacheFolder;
                                               //dayObject.DayImagePath = $"{folder.Path}\\{dayObject.Vol }.jpg";
                                               //dayObject.DayImagePath = TempPath;
                    dayObjectCollection.Add(dayObject);
                }
            }
            return dayObjectCollection;
        }
    }

    class DayObject
    {
        private string _vol;

        private string _headerString;

        private string _mainString;

        private string _byWho;

        private string _dayImagePath;

        private string _oneDay;

        private string _oneMonthAndYear;

        private string _mainContent;

        public string MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; }
        }


        public string Vol
        {
            get
            {
                return _vol;
            }

            set
            {
                _vol = value;
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
            }
        }

    }

}
