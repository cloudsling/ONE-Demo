using JYAnalyticsUniversal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.Web.Http;
using static Demo.LastUpdate;

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

        public async void OneMainSavePic()
        {
            int index = fv.SelectedIndex;
            try
            {
                await GetOne.SavePic(Main.DayObjectCollection[index].Vol + ".jpg", "ONE." + Main.DayObjectCollection[index].Vol + "首页.jpg");
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
            if (StartPage.IsFromInternet)
            {
                if (Main.dayReallyObjectString == null)
                {
                    try
                    {
                        Main.dayReallyObjectString = await Main.GetDayReallyObjectString();
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        System.Diagnostics.Debug.Write("|kkkkkkkkkkk|    " + ex.Message + "|assssssssssss");
#endif

                    }
                }
                if (Main.dayReallyObject == null)
                {
                    Main.dayReallyObject = new DayReallyObject();
                    try
                    {
                        await Task.Factory.StartNew(() =>
                         Main.dayReallyObject = GetOne.GetTodayReallyObject(Main.dayReallyObjectString));
                    }
                    catch (Exception ex)
                    {

#if DEBUG
                        System.Diagnostics.Debug.Write("|kkkkkkkkkkk|    " + ex.Message + "|assssssssssss");
                        throw;
#endif
                        
                    }
                }
            }
            else
            {
                Main.dayReallyObjectString = await LoadDayObj();
                Main.dayReallyObject = new DayReallyObject();
                Main.dayReallyObject.OneQuestion = JsonConvert.DeserializeObject<OneQuestionObject>(await LoadSth("question.txt"));
                Main.dayReallyObject.Articles = JsonConvert.DeserializeObject<ArticlesObject>(await LoadSth("article.txt"));
                await Task.Delay(2000);
                Main.NotifyUserMethod("离线模式已启动", 200);
            }
            if (e.Parameter != null)
            {
                var temp = e.Parameter as DayObject;
                if (temp != null)
                {
                    var bind = fv.ItemsSource as ImageBinding;
                    if (bind != null)
                    {
                        bind.AddImageBinding(temp.DayImagePath);
                    }
                    fv.SelectedIndex = 0; 
                     ChangeMainCurrentMsg(temp);
                }
            }
            JYAnalytics.TrackPageStart("main_page");
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            JYAnalytics.TrackPageEnd("main_page");
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
            oneMainViewModel.PageDataBinding.ByWho = obj.ByWho;
            oneMainViewModel.PageDataBinding.DayImagePath = obj.DayImagePath;
            oneMainViewModel.PageDataBinding.HeaderString = obj.HeaderString;
            oneMainViewModel.PageDataBinding.MainString = obj.MainString;
            oneMainViewModel.PageDataBinding.Vol = obj.Vol;
            oneMainViewModel.PageDataBinding.OneDay = obj.OneDay;
            oneMainViewModel.PageDataBinding.OneMonthAndYear = obj.OneMonthAndYear;
            oneMainViewModel.PageDataBinding.PraiseName = obj.PraiseName;
            oneMainViewModel.PageDataBinding.ItemId = obj.ItemId;
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

        private async void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            int index = fv.SelectedIndex;
            try
            {
                await GetOne.SavePic(FlipViewImageSourceList[index], "ONE." + Main.DayObjectCollection[index].Vol + "首页.jpg");
                Main.NotifyUserMethod(@"成功保存至Pictures\ONE-一个文件夹", 360);
            }
            catch (Exception)
            {
                Main.NotifyUserMethod("保存失败！", 180);
            }
        }

        Dictionary<string, bool> isClickDic = new Dictionary<string, bool>();
        async void Love_Click(object sender, RoutedEventArgs e)
        {
            if (isClickDic.ContainsKey(oneMainViewModel.PageDataBinding.ItemId))
            {
                int result;
                if (int.TryParse(oneMainViewModel.PageDataBinding.PraiseName, out result))
                {
                    oneMainViewModel.PageDataBinding.PraiseName = (result -= 1).ToString();
                }
                isClickDic.Remove(oneMainViewModel.PageDataBinding.ItemId);
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("UserAgent", "android-async-http/2.0 (http://loopj.com/android-async-http)");
                client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    {"itemid", oneMainViewModel.PageDataBinding.ItemId},
                    {"type","hpcontent" },
                    {"deviceid","00000000-0565-4187-0033-c5870033c587" },
                    {"devicetype","android"}
                };
                HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(dic);

                var response = await client.PostAsync(new Uri("http://v3.wufazhuce.com:8000/api/praise/add"), content);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    isClickDic.Add(oneMainViewModel.PageDataBinding.ItemId, true);
                    Main.NotifyUserMethod("喜欢成功", 150);
                    int result;
                    if (int.TryParse(oneMainViewModel.PageDataBinding.PraiseName, out result))
                    {
                        oneMainViewModel.PageDataBinding.PraiseName = (result += 1).ToString();
#if DEBUG
                        await new MessageDialog("Success!").ShowAsync();
#endif
                    }
                }
                //  await response.Content.ReadAsStringAsync();
            }
        }
    }

    public class OneMainViewModel
    {

        public OneMainViewModel()
        {
            PageDataBinding = new DayObject();
            //if (DesignMode.DesignModeEnabled)
            //{
            //    PageDataBinding = new DayObject
            //    {
            //        PraiseName = "12324",
            //        ByWho = "qwe",
            //        HeaderString = "qwew",
            //        MainString = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq",
            //        OneMonthAndYear = "2016-2",
            //        OneDay = "12",
            //        Vol = "VOL.1225"
            //    };

            //}
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

        public void AddImageBinding(string newone)
        {
            this.Insert(0, new ImageSource { Source0 = newone });
        }
    }

}
