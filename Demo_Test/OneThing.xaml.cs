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

namespace Demo
{
    public sealed partial class OneThing : Page
    {
        public OneThingViewModel oneThingViewModel { get; set; }
        public OneThing()
        {
            oneThingViewModel = new OneThingViewModel();
            InitializeComponent();
            Main.OtherPageDoWhenThemeChanged = () => ThemeColorModel.InitialByOtherObject(oneThingViewModel.OneThingColorModel, ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            oneThingViewModel.OneThingColorModel = ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme);
            Main.CreateFileButtonClick = OneThingSavePic;
            //InitializationOneThingObject(Main.dayReallyObject.OneThing);
        }
        /// <summary>
        /// 初始化OneThing
        /// </summary>
        /// <param name="oneThing"></param>
        void InitializationOneThingObject(OneThingObject oneThing)
        {
            oneThingViewModel.oneThing.ImagePath = oneThing.ImagePath;
            oneThingViewModel.oneThing.Content = oneThing.Content;
            oneThingViewModel.oneThing.Header = oneThing.Header;
        }
        public async void OneThingSavePic()
        {
            try
            {
                await GetOne.SavePic(oneThingViewModel.oneThing.ImagePath, Main.DayObjectCollection[0].Vol + "ONE-东西.jpg");
                Main.NotifyUserMethod(@"成功保存至Pictures\ONE-一个文件夹", 360);
            }
            catch (Exception)
            {
                Main.NotifyUserMethod("保存失败！", 180);
            }
        }
    }

    public class OneThingViewModel
    {
        public OneThingViewModel()
        {
            oneThing = new OneThingObject();
        }
        public OneThingObject oneThing { get; set; }

        public ThemeColorModel OneThingColorModel { get; set; }

    }
}
