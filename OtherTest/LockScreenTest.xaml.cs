using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OtherTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LockScreenTest : Page
    {
        public LockScreenTest()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!UserProfilePersonalizationSettings.IsSupported())
            {
                await new MessageDialog("not suppost!!").ShowAsync();
                return;
            }
            var settings = UserProfilePersonalizationSettings.Current;
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.GetFileAsync("VOL.1231.jpg");
            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/1.png"));
            bool b = await settings.TrySetLockScreenImageAsync(file);
            if (b)
            {
                await new MessageDialog("Success！").ShowAsync();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ITileWide310x150SmallImageAndText03 tileContent = TileContentFactory.CreateTileWide310x150SmallImageAndText03();
            //tileContent.TextBodyWrap.Text = "This tile notification has an image, but it won't be displayed on the lock screen";
            //tileContent.Image.Src = "ms-appx:///images/tile-sdk.png";
            //tileContent.RequireSquare150x150Content = false;
            //TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());

        }
        const string TileTempleXml = @"
<tile>
  <visual>

    <binding>
      <text>11111111111111111111111111</text>
    </binding>

  </visual>
</tile>"
;

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string tileXmlString = "<tile>"
                                + "<visual version='2'>"
                                + "<binding template='TileWide310x150SmallImageAndText03' >"
                                + "<text id='1'>qqqqqqqqqqqqqqqqqqqqq</text>"
                                + "</binding>"
                                + "</visual>"
                                + "</tile>";
            XmlDocument doc = new XmlDocument();
            //   string xml = string.Format(TileTempleXml);
            doc.LoadXml(tileXmlString, new XmlLoadSettings
            {
                ProhibitDtd = false,
                ValidateOnParse = false,
                ElementContentWhiteSpace = false,
                ResolveExternals = false
            });

            TileNotification notificate = new TileNotification(doc);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notificate);

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
        //async Task<string> UpdateLockScreenImage()
        //{
        //    if (!UserProfilePersonalizationSettings.IsSupported())
        //    {
        //        return null;
        //    }
        //    var settings = UserProfilePersonalizationSettings.Current;
        //    StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
        //    await settings.TrySetLockScreenImageAsync(file);
        //    return null;
        //}
    }
}
