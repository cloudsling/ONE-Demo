using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Demo
{
    public sealed partial class OneMain : Page
    {
        public OneMainViewModel oneMainViewModel { get; set; }
        public static OneMain OneMainCurrent;
        public OneMain()
        {
            oneMainViewModel = new OneMainViewModel();
            InitializeComponent();
            OneMainCurrent = this;
            Main.OtherPageDoWhenThemeChanged = () => ThemeColorModel.InitialByOtherObject(oneMainViewModel.ThemeColorModel, ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme));
        }

        public static List<string> FlipViewImageSourceList;

        public void OneMainSavePic()
        {
            int index = fv.SelectedIndex;
            try
            {
                GetOne.SavePic(Main.DayObjectCollection[index].Vol + ".jpg", "ONE." + Main.DayObjectCollection[index].Vol + "首页.jpg");
                Main.NotifyUserMethod(@"成功保存至 Pictures\ONE-一个 文件夹", 360);
            }
            catch (Exception)
            {
                Main.NotifyUserMethod("保存失败！", 180);
            }
        }

        /// <summary>
        /// OnNavigatedTo方法
        /// </summary>
        /// <param name="e"></param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Main.MainCurrent.mainViewModel.oneSettings.OneMainPageStyle == 0)
            {
                OneMainStyle2.Visibility = Visibility.Visible;
                OneMainStyle1.Visibility = Visibility.Collapsed;
            }
            else
            {
                OneMainStyle1.Visibility = Visibility.Visible;
                OneMainStyle2.Visibility = Visibility.Collapsed;
            }
            oneMainViewModel.ThemeColorModel = ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme);
            StoryStart.Begin();
            Main.CreateFileButtonClick = OneMainSavePic;
            FlipViewImageSourceList = GetOne.GetOneTodayImageSourceAsync(Main.x);
            if (this.fv.ItemsSource == null)
            {
                ImageBinding bind = new ImageBinding();
                this.fv.ItemsSource = bind;
            }
            ChangeAllEllipseColor(0);

            if (Main.dayReallyObjectString == null)
            {
                Main.dayReallyObjectString = await Main.GetDayReallyObjectString(Main.DayObjectCollection[0].Vol);
            }
            if (Main.dayReallyObject == null)
            {
                Main.dayReallyObject = GetOne.GetTodayReallyObject(Main.dayReallyObjectString);
            }

        }

        #region 圆点方法

        /// <summary>
        /// 改变一个圆点的颜色
        /// </summary>
        /// <param name="ellipse"></param>
        private void ChangeEllipseColor(Ellipse ellipse, Color color)
        {
            ellipse.Fill = new SolidColorBrush(color);
        }
        /// <summary>
        /// 改变所有的圆点的颜色
        /// </summary>
        /// <param name="CurrentIndex"></param>
        private void ChangeAllEllipseColor(int CurrentIndex)
        {
            //SolidColorBrush s = new SolidColorBrush ()
            var childrens = ManyEllipse.Children;
            var index = 0;
            foreach (var child in childrens)
            {
                if (index == CurrentIndex)
                {
                    ChangeEllipseColor((child as Ellipse), Colors.SkyBlue);
                }
                else
                {
                    ChangeEllipseColor((child as Ellipse), Colors.White);
                }
                index++;
            }
        }

        #endregion

        private void fv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Threading.Tasks.Task.Delay(1000);
            var flipview = sender as FlipView;
            int index = flipview.SelectedIndex;

            //PageDataBinding = null;
            if (index < 0 || index > 6) return;
            ChangeAllEllipseColor(index);
            ChangeMainCurrentMsg(Main.DayObjectCollection[index]);
        }
        /// <summary>
        /// 初始化对象，应该用字段更好
        /// </summary>
        /// <param name="obj"></param>
        public void ChangeMainCurrentMsg(DayObject obj)
        {
            oneMainViewModel.PageDataBinding.ByWho = obj.ByWho;
            oneMainViewModel.PageDataBinding.DayImagePath = obj.DayImagePath;
            oneMainViewModel.PageDataBinding.HeaderString = obj.HeaderString;
            oneMainViewModel.PageDataBinding.MainString = obj.MainString;
            oneMainViewModel.PageDataBinding.Vol = obj.Vol;
            oneMainViewModel.PageDataBinding.OneDay = obj.OneDay;
            oneMainViewModel.PageDataBinding.OneMonthAndYear = obj.OneMonthAndYear;
        }

        #region 没什么卵用的方法

        private void GoLeft_Click(object sender, RoutedEventArgs e)
        {
            if (fv.SelectedIndex > 0)
            {
                fv.SelectedIndex = fv.SelectedIndex - 1;
            }
        }

        private void GoRight_Click(object sender, RoutedEventArgs e)
        {
            if (fv.SelectedIndex < 6)
            {
                fv.SelectedIndex = fv.SelectedIndex + 1;
            }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = fv.SelectedIndex;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Refreshen();
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            int index = fv.SelectedIndex;
            try
            {
                GetOne.SavePic(FlipViewImageSourceList[index], "ONE." + Main.DayObjectCollection[index].Vol + "首页.jpg");
                Main.NotifyUserMethod(@"成功保存至Pictures\ONE-一个文件夹", 360);
            }
            catch (Exception)
            {
                Main.NotifyUserMethod("保存失败！", 180);
            }
        }
    }

    public class OneMainViewModel
    {

        public OneMainViewModel()
        {
            PageDataBinding = new DayObject();
        }

        DayObject _pageDataBinding;

        public DayObject PageDataBinding
        {
            get
            {
                return _pageDataBinding;
            }

            set
            {
                _pageDataBinding = value;
            }
        }

        public ThemeColorModel ThemeColorModel
        {
            get
            {
                return themeColorModel;
            }

            set
            {
                themeColorModel = value;
            }
        }

        ThemeColorModel themeColorModel;
    }


    public class ImageSource
    {
        private string _source0;

        public string Source0
        {
            get
            {
                return _source0;
            }

            set
            {
                _source0 = value;
            }
        }
    }

    public class ImageBinding : ObservableCollection<ImageSource>
    {
        public ImageBinding()
        {
            List<string> list = OneMain.FlipViewImageSourceList;
            for (int i = 0; i < 7; i++)
            {
                this.Add(
                    new ImageSource
                    {
                        Source0 = list[i]
                    }
                    );
            }
        }
    }

}
