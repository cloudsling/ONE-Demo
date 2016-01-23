using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class OneThing : Page
    {
        public OneThingObject oneThing { get; set; }

        public OneThing()
        {
            oneThing = new OneThingObject();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Main.CreateFileButtonClick = OneThingSavePic;
            InitializationOneThingObject(Main.dayReallyObject.OneThing);
        }
        /// <summary>
        /// 初始化OneThing
        /// </summary>
        /// <param name="oneThing"></param>
        private void InitializationOneThingObject(OneThingObject oneThing)
        {
            this.oneThing.ImagePath = oneThing.ImagePath;
            this.oneThing.Content = oneThing.Content;
            this.oneThing.Header = oneThing.Header;
        }
        public void OneThingSavePic()
        {
            try
            {
                GetOne.SavePic(oneThing.ImagePath, Main.DayObjectCollection[0].Vol + "ONE-东西.jpg");
                Main.NotifyUserMethod(@"成功保存至Pictures\ONE-一个文件夹", 360);
            }
            catch (Exception)
            {
                Main.NotifyUserMethod("保存失败！", 180);
            }
        }
    }
}
