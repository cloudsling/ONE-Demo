using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public DayObject PageDataBinding { get; set; }

        public static OneMain OneMainCurrent;
        public OneMain()
        {
            PageDataBinding = new DayObject();
            this.InitializeComponent();
            OneMainCurrent = this;
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
        }

        public static List<string> FlipViewImageSourceList;

        public void OneMainSavePic()
        {
            int index = fv.SelectedIndex;
            try
            {
                GetOne.SavePic(FlipViewImageSourceList[index], "ONE." + Main.DayObjectCollection[index].Vol + "首页.jpg");
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
            PageDataBinding.ByWho = obj.ByWho;
            PageDataBinding.DayImagePath = obj.DayImagePath;
            PageDataBinding.HeaderString = obj.HeaderString;
            PageDataBinding.MainString = obj.MainString;
            PageDataBinding.Vol = obj.Vol;
            PageDataBinding.OneDay = obj.OneDay;
            PageDataBinding.OneMonthAndYear = obj.OneMonthAndYear;
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
