using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.Web.Http;

namespace Demo.BackGround
{
    public sealed class SetLockScreenTask : IBackgroundTask
    {
        static string uri = "http://139.129.116.86:8000/api/hp/more/0?";

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            //TODO something
            await GetLastContent();
            deferral.Complete();
        }

        IAsyncOperation<string> GetLastContent()
        {
            try
            {
                return AsyncInfo.Run(token => SetLockScreenMethod());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> SetLockScreenMethod()
        {
            StorageFile file = await GetImagePath();
            await SetLockScreenCore(file);
            return null;
        }
        //private void TrySetLockScreenNotification(string content)
        //{
        //    string tileXmlString = "<tile>"
        //                          + "<visual version='2'>"
        //                          + "<binding template='TileWide310x150SmallImageAndText03' fallback='TileWideSmallImageAndText03'>"
        //                          + "<text id='1' hint-wrap='true'>{0}</text>"
        //                          + "</binding>"
        //                          + "</visual>"
        //                          + "</tile>";
        //    XmlDocument doc = new XmlDocument();
        //    string xml = string.Format(tileXmlString, content);
        //    doc.LoadXml(xml, new XmlLoadSettings
        //    {
        //        ProhibitDtd = false,
        //        ValidateOnParse = false,
        //        ElementContentWhiteSpace = false,
        //        ResolveExternals = false
        //    });

        //    TileNotification notificate = new TileNotification(doc);
        //    TileUpdateManager.CreateTileUpdaterForApplication().Update(notificate);
        //}

        private async Task<StorageFile> GetImagePath()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(new Uri(uri));

                string[] temp = GetOneTodayObjectList(response);
                var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(temp[0]);
                if (item != null)
                {
                    var file = await ApplicationData.Current.LocalFolder.GetFileAsync(temp[0]);
                    return file;
                }
                else
                {
                    await SavePicHere(temp[0], temp[1]);
                    StorageFile file = await ApplicationData.Current.LocalCacheFolder.GetFileAsync(temp[0]);
                    var copyFile = await file.CopyAsync(ApplicationData.Current.LocalFolder);
                    return copyFile;
                }
            }
        }

        async Task SetLockScreenCore(StorageFile file)
        {
            if (!UserProfilePersonalizationSettings.IsSupported())
            {
                return;
            }
            var settings = UserProfilePersonalizationSettings.Current;

            await settings.TrySetLockScreenImageAsync(file);

        }

        string[] GetOneTodayObjectList(string resultString)
        {
            //CutOneString(ref resultString);
            JsonObject json;
            string picPath;
            if (JsonObject.TryParse(resultString, out json))
            {
                JsonArray oneMainContentCollection = json.GetNamedArray("data");

                JsonObject item = oneMainContentCollection[0].GetObject();
                //DayObject dayObject = new DayObject();
                picPath = item.GetNamedString("hp_img_original_url");
                //TrySetLockScreenNotification(item.GetNamedString("hp_content"));
                //  dayObject.ByWho = item.GetNamedString("hp_author") ?? "可能服务器出现故障-_-";
                string vol = item.GetNamedString("hp_title") ?? "可能服务器出现故障-_-";
                // dayObject.HeaderString = "无题";
                //string[] temp = item.GetNamedString("hp_makettime")?.Split(' ');
                //if (temp.Length == 0)
                //{
                //    dayObject.OneDay = "可能服务器出现故障-_-";
                //}
                // dayObject.OneMonthAndYear = temp[0].Substring(0, temp[0].LastIndexOf('-'));
                //  dayObject.OneDay = temp[0];//.Substring(temp[0].LastIndexOf('-') + 1);
                //var folder = ApplicationData.Current.LocalCacheFolder;
                //dayObject.DayImagePath = $"{folder.Path}\\{dayObject.Vol }.jpg";
                //dayObject.DayImagePath = TempPath;
                return new string[] { vol + ".jpg", picPath };
            }
            return null;
        }

        private async Task SavePicHere(string picPath, string v)
        {
            using (HttpClient client = new HttpClient())
            {
                IBuffer buffer = await client.GetBufferAsync(new Uri(picPath));
                StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(v);
                await FileIO.WriteBufferAsync(file, buffer);
            }
        }
    }
}
