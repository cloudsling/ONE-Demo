using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using static Demo.LastUpdate;
using Demo;

namespace Demo
{
    /// <summary>
    /// 事件
    /// </summary>
    public sealed partial class Main : Page
    {
        void AppBarButton_Click(object sender, RoutedEventArgs e) => Refreshen();

        void AppBar_LostFocus(object sender, RoutedEventArgs e) => AppBar.IsOpen = false;

        void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreateFileButtonClick != null) CreateFileButtonClick();
            AppBar.IsOpen = false;
        }

        void HamBtn_Click(object sender, RoutedEventArgs e) => SwitchSplitter();
        
        async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GiveMeStarOk.Begin();
            if ((string)(sender as Button).Tag == "DontDo") goto id;
            await Task.Delay(650);
            Love();
            id:
            StarStar.Visibility = Visibility.Collapsed;
            GaoStatusBar.ShowStatusBar();
        }

        void DemoDemo_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e) => SwitchSplitter();

        void SunOrNightMode_Click(object sender, RoutedEventArgs e)
        {
            var vis = SunMode.Visibility;
            SunMode.Visibility = NightMode.Visibility;
            NightMode.Visibility = vis;
            if (SunMode.Visibility == Visibility.Collapsed)
            {
                mainViewModel.oneSettings.RequireLightTheme = false;
                ThemeColorModel.InitialByOtherObject(mainViewModel.themeColorModelSettings, new ThemeColorModel(false));
                GaoStatusBar.SetStatusBar(Colors.White, ThemeColorModel.NightModeTheme.StatusBarBackGroundColor.Color);
            }
            else {
                mainViewModel.oneSettings.RequireLightTheme = true;
                ThemeColorModel.InitialByOtherObject(mainViewModel.themeColorModelSettings, new ThemeColorModel(true));
                GaoStatusBar.SetStatusBar(Colors.White, ThemeColorModel.SunModeTheme.StatusBarBackGroundColor.Color);
            }
            OtherPageDoWhenThemeChanged();
        }

        void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            isHide.Visibility = Visibility.Collapsed;
            txtSearch.Visibility = Visibility.Visible;
            txt_Search.Focus(FocusState.Keyboard);
        }

        void txt_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_Search.Focus(FocusState.Keyboard);
            if (string.IsNullOrEmpty(txt_Search.Text))
            {
                OneFrame.Navigate(typeof(SearchPage)); return;
            }
            if (OneFrame.CurrentSourcePageType != typeof(SearchPage))
            {
                OneFrame.Navigate(typeof(SearchPage));
            }
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Visibility = Visibility.Collapsed;
            isHide.Visibility = Visibility.Visible;
        }

        void txt_Search_LostFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Visibility = Visibility.Collapsed;
            isHide.Visibility = Visibility.Visible;
            var box = sender as AutoSuggestBox;
            if (string.IsNullOrEmpty(box.Text))
            {
                do
                {
                    OneFrame.GoBack();
                } while (OneFrame.CurrentSourcePageType == typeof(SearchPage));
            }
        }

        async void txt_Search_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            await SearchPage.searchPageCurrent.SearchStart(sender.Text);
        }

        void OneFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Splitter.IsPaneOpen = false;
            isHide.Visibility = Visibility.Visible;
            HamListBox.SelectedItem = null;
            switch (OneFrame.CurrentSourcePageType.Name)
            {
                case nameof(OneMain):
                    CreateFileButton.Visibility = Visibility.Visible;
                    AppBar.Visibility = Visibility.Visible;
                    HEADER111.Text = "ONE- 首页";
                    break;
                case nameof(Articles):
                    CreateFileButton.Visibility = Visibility.Collapsed; //isHide.Visibility = Visibility.Visible;
                    AppBar.Visibility = Visibility.Visible;
                    HEADER111.Text = "ONE- 文章";
                    break;
                case nameof(OneQuestion):
                    CreateFileButton.Visibility = Visibility.Collapsed; //isHide.Visibility = Visibility.Visible;
                    AppBar.Visibility = Visibility.Visible;
                    HEADER111.Text = "ONE- 问题";
                    break;
                case nameof(Serial):
                    CreateFileButton.Visibility = Visibility.Collapsed;
                    HEADER111.Text = "ONE- 连载";
                    AppBar.Visibility = Visibility.Collapsed; //isHide.Visibility = Visibility.Visible;
                    break;
                case nameof(Music):
                    CreateFileButton.Visibility = Visibility.Collapsed;
                    HEADER111.Text = "ONE- 音乐";
                    AppBar.Visibility = Visibility.Collapsed;// isHide.Visibility = Visibility.Visible;
                    break;
                case nameof(Movie):
                    CreateFileButton.Visibility = Visibility.Collapsed;
                    HEADER111.Text = "ONE- 电影";
                    AppBar.Visibility = Visibility.Collapsed;// isHide.Visibility = Visibility.Visible;
                    break;
                case nameof(SearchPage):
                    CreateFileButton.Visibility = Visibility.Collapsed;
                    HEADER111.Text = "搜索";
                    AppBar.Visibility = Visibility.Collapsed; isHide.Visibility = Visibility.Collapsed;
                    break;
                case nameof(SharePage):
                    CreateFileButton.Visibility = Visibility.Collapsed;
                    HEADER111.Text = "搜索";
                    AppBar.Visibility = Visibility.Collapsed; isHide.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void HamListBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            MyListViewItems m = e.ClickedItem as MyListViewItems;
            if (m != null)
            {
                //HEADER111.Text = "ONE- " + m.MyItemName.Trim();
                //if (isHide.Visibility == Visibility.Collapsed) isHide.Visibility = Visibility.Visible;
                OneFrame.Navigate(m.ClassType);
                NotifyUserMethod("ONE-" + m.MyItemName, 180);
            }
            HamListBox.SelectedItem = null;
        }

        private void Settings_ItemClick(object sender, ItemClickEventArgs e)
        {
            SwitchSplitter();
            AppBar.Visibility = Visibility.Collapsed;
            // HEADER111.Text = "设置";
            OneFrame.Navigate(typeof(Settings));
            isHide.Visibility = Visibility.Collapsed;
            Settings.SelectedItem = null;
        }

        private void Others_ItemClick(object sender, ItemClickEventArgs e)
        {
            SwitchSplitter();
            HEADER111.Text = "关于";
            AppBar.Visibility = Visibility.Collapsed;
            if (isHide.Visibility == Visibility.Visible) { isHide.Visibility = Visibility.Collapsed; }
            OneFrame.Navigate(typeof(About));
            Others.SelectedItem = null;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Love();
        }

        double cumulateX = 0;
        double cumulateY = 0;

        private void Grid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            ShareGrid.SetValue(Canvas.TopProperty, e.Cumulative.Translation.Y + cumulateY);
            ShareGrid.SetValue(Canvas.LeftProperty, e.Cumulative.Translation.X + cumulateX);
        }

        private void Grid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            cumulateX = cumulateX + e.Cumulative.Translation.X;
            cumulateY = cumulateY + e.Cumulative.Translation.Y;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OneFrame.Navigate(typeof(SharePage),Main.DayObjectCollection[OneMain.OneMainCurrent.flipview.SelectedIndex]);
        }

        private async void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9NBLGGH4P3BT"));

            mainViewModel.oneSettings.RemindDownloadNewVersion += 1;
            DownloadStoryOk.Begin();
        }

        private void Gun_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.oneSettings.RemindDownloadNewVersion += 1;
            DownloadStoryOk.Begin();
        }
    }

    /// <summary>
    /// 入口和方法
    /// </summary>
    public sealed partial class Main : Page
    {
        public Main()
        {
            mainViewModel = new MainViewModel();
            InitializeComponent();
            MainCurrent = this;
        }

        async void Main_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (mainViewModel.oneSettings.IsDoubleClickExit)
            {
                if (OneFrame.CurrentSourcePageType == typeof(OneMain))
                {
                    e.Handled = true;
                    if (Math.Abs(mainViewModel.SomethingInMainSettings.NotifyUserWidth - 201) <= 1.0) Application.Current.Exit();
                    NotifyUserMethod("再按一次后退键退出程序", 201);
                    await Task.Delay(2000);
                    mainViewModel.SomethingInMainSettings.NotifyUserWidth = 180;
                    return;
                }
            }
            else if (OneFrame.CurrentSourcePageType == typeof(OneMain)) return;
            if (OneFrame.CanGoBack)
            {
                Type temp;//= OneFrame.CurrentSourcePageType;
                do
                {
                    temp = OneFrame.CurrentSourcePageType;
                    OneFrame.GoBack();
                } while (Equals(temp, OneFrame.CurrentSourcePageType));
                if (OneFrame.CurrentSourcePageType == typeof(BlankPage))
                    OneFrame.GoBack();
                e.Handled = true;
            }
        }

        async static Task<string> GetOneString(string uri)
        {
            if (httpClient != null) httpClient.Dispose();
            using (httpClient = new HttpClient())
                x = await httpClient.GetStringAsync(new Uri(uri));
            await SaveX(x);
            return x;
        }

        public async static Task<string> GetDayReallyObjectString()
        {
            StringBuilder sb = new StringBuilder();
            //if (httpClient != null) httpClient.Dispose();
            //string temp = "";
            //string sd = uriOneQuestion + vol;
            using (httpClient = new HttpClient())
            {
                sb.Append(await httpClient.GetStringAsync(new Uri(GetOne.GetOneQestionUri())));
            }
            //if (httpClient != null) httpClient.Dispose();
            //using (httpClient = new HttpClient())
            //{
            //    sb.Append(await httpClient.GetStringAsync(new Uri(GetOne.GetArticleUri(x))));
            //}
            await SaveDayObj(sb.ToString());
            return sb.ToString();
        }

        public void ChangeSunOrNightMode(bool RequireLightTheme)
        {
            if (!RequireLightTheme)
            {
                NightMode.Visibility = Visibility.Visible;
                SunMode.Visibility = Visibility.Collapsed;
            }
            else
            {
                SunMode.Visibility = Visibility.Visible;
                NightMode.Visibility = Visibility.Collapsed;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += Main_BackRequested;
            mainViewModel.themeColorModelSettings = ThemeColorModel.GetTheme(mainViewModel.oneSettings.RequireLightTheme);
            ChangeSunOrNightMode(mainViewModel.oneSettings.RequireLightTheme);
            GaoStatusBar.SetStatusBar(Colors.White, mainViewModel.themeColorModelSettings.StatusBarBackGroundColor.Color);
            GaoStatusBar.SetStatusBarProgressIndicator(0);
            if ((e.Parameter as string) == "404")
            {
                HEADER111.Text = "关于";
                if (isHide.Visibility == Visibility.Visible)
                    isHide.Visibility = Visibility.Collapsed;
                OneFrame.Navigate(typeof(About));
                await Task.Delay(2000);
                NotifyUserMethod("程序将在5秒后退出", 220);
                await Task.Delay(5000);
                Application.Current.Exit();
            }
            else
            {
                HamListBox.ItemsSource = new ListViewTiemsBinding();
                var obj = e.Parameter as MyNavigationEventArgs;
                if (obj != null)
                {
                    x = obj.XX;
                    DayObjectCollection = obj.dayObjectCollection;
                }
                ring.IsActive = false;
                OneFrame.Navigate(typeof(OneMain));
                StorageFolder storageFolder = KnownFolders.PicturesLibrary;
                oneFolder = await storageFolder.CreateFolderAsync("ONE-一个", CreationCollisionOption.OpenIfExists);
            }

            mainViewModel.oneSettings.GiveMeGood += 1;
            if (mainViewModel.oneSettings.GiveMeGood == 10)
            {
                if (cb.IsChecked == false) mainViewModel.oneSettings.GiveMeGood = 0;
                GaoStatusBar.HideStatusBar();
                StarStar.Visibility = Visibility.Visible;
                GiveMeStar.Begin();
            }

            if (mainViewModel.oneSettings.GiveMeGood == 10)
            {
                if (cb.IsChecked == false) mainViewModel.oneSettings.GiveMeGood = 0;
                GaoStatusBar.HideStatusBar();
                StarStar.Visibility = Visibility.Visible;
                GiveMeStar.Begin();
            }
            if (mainViewModel.oneSettings.RemindDownloadNewVersion == 1)
            { 
                GaoStatusBar.HideStatusBar();
                downloadGrid.Visibility = Visibility.Visible;
                DownloadStory.Begin();
            }
        }

        /// <summary>
        /// 刷新首页
        /// </summary>
        /// 
        public async static void Refreshen()
        {
            if (!StartPage.IsFromInternet)
            {
                NotifyUserMethod("当前处于离线模式", 200);
                return;
            }
            GaoStatusBar.SetStatusBarProgressIndicator(null, "正在刷新");
            await Task.Delay(50);
            var currentFrame = MainCurrent.OneFrame.CurrentSourcePageType;
            MainCurrent.OneFrame.Navigate(typeof(BlankPage));
            try
            {
                Task task = await Task.Factory.StartNew(async () =>
                {
                    await GetOneString(uri);
                    DayObjectCollection = GetOne.GetOneTodayObjectList(x);
                }, TaskCreationOptions.DenyChildAttach);
                Task.WaitAll(task);
            }
            catch (Exception)
            {
                await new MessageDialog("opps！请检查宁德网络连接，离线版本将在后续版本开发o((⊙﹏⊙))o.").ShowAsync();
                NotifyUserMethod("刷新失败！", 180);
            }
            MainCurrent.OneFrame.Navigate(currentFrame);
            GaoStatusBar.SetStatusBarProgressIndicator(0);
            await Task.Delay(800);
            NotifyUserMethod("刷新成功！", 180);
        }
        /// <summary>
        /// 因垂丝汀的通知方法
        /// </summary>
        /// <param name="notifyUserText"></param>
        /// <param name="NotifyUserWidth"></param>
        public static void NotifyUserMethod(string notifyUserText, double NotifyUserWidth)
        {
            MainCurrent.NotifyUserText.Text = notifyUserText;
            MainCurrent.mainViewModel.SomethingInMainSettings.NotifyUserWidth = NotifyUserWidth;
            MainCurrent.NotifyAnime.Begin();
        }


        /// <summary>
        /// 关闭或者呼出汉堡菜单
        /// </summary>
        private void SwitchSplitter() => Splitter.IsPaneOpen = !Splitter.IsPaneOpen;

        public void ChangeIsHide()
        {
            if (isHide.Visibility != Visibility.Collapsed) isHide.Visibility = Visibility.Visible;
            else {
                isHide.Visibility = Visibility.Collapsed;
            }
        }

        async void Love() => await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9NBLGGH58VLR"));

        public void ThisPageDoWhenThemeChanged()
        {
            ChangeSunOrNightMode(mainViewModel.oneSettings.RequireLightTheme);
            if (SunMode.Visibility == Visibility.Collapsed)
            {
                mainViewModel.oneSettings.RequireLightTheme = false;
                ThemeColorModel.InitialByOtherObject(mainViewModel.themeColorModelSettings, new ThemeColorModel(false));
                GaoStatusBar.SetStatusBar(Colors.White, ThemeColorModel.NightModeTheme.StatusBarBackGroundColor.Color);
            }
            else {
                mainViewModel.oneSettings.RequireLightTheme = true;
                ThemeColorModel.InitialByOtherObject(mainViewModel.themeColorModelSettings, new ThemeColorModel(true));
                GaoStatusBar.SetStatusBar(Colors.White, ThemeColorModel.SunModeTheme.StatusBarBackGroundColor.Color);
            }
        }
    }

    /// <summary>
    /// 字段和属性
    /// </summary>
    public sealed partial class Main : Page
    {
        public MainViewModel mainViewModel { get; set; }
        public static Main MainCurrent;
        public static List<DayObject> DayObjectCollection;
        public static DayReallyObject dayReallyObject;
        public static StorageFolder oneFolder;
        public static HttpClient httpClient;
        public static List<string> imageSourceAsync;
        public static string itemPath;
        public static string x;
        public static string uri = "http://139.129.116.86:8000/api/hp/more/0?";
        public static string uriOneArticle = "http://wufazhuce.com/article/";
        public static string uriOneQuestion = "http://wufazhuce.com/question/";
        public static string dayReallyObjectString;
        public static Action CreateFileButtonClick;
        public static Action OtherPageDoWhenThemeChanged;
    }

    public class MainViewModel
    {
        public MainViewModel()
        {
            SomethingInMainSettings = new SomethingInMain();
            oneSettings = new OneSettings();
        }
        //private double _notifyUserWidth;
        public SomethingInMain SomethingInMainSettings { get; set; }

        public OneSettings oneSettings { get; set; }

        public ThemeColorModel themeColorModelSettings { get; set; }
    }

    public class SomethingInMain : INotifyPropertyChanged
    {
        private double _notifyUserWidth;

        public double NotifyUserWidth
        {
            get
            {
                return _notifyUserWidth;
            }

            set
            {
                _notifyUserWidth = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChangedEventHandler temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propName));
        }
    }
    public class MyListViewItems
    {
        string _MyitemName;
        public string MyItemName
        {
            get
            {
                return _MyitemName;
            }

            set
            {
                _MyitemName = value;
            }
        }
        string _MyGlyph;
        public string MyGlyph
        {
            get
            {
                return _MyGlyph;
            }

            set
            {
                _MyGlyph = value;
            }
        }
        SolidColorBrush _BgColor;
        public SolidColorBrush BgColor
        {
            get
            {
                return _BgColor;
            }

            set
            {
                _BgColor = value;
            }
        }
        public Type ClassType { get; set; }
    }

    public class ListViewTiemsBinding : ObservableCollection<MyListViewItems>
    {
        public ListViewTiemsBinding()
        {
            this.Add(new MyListViewItems
            {
                MyItemName = "   首页",
                ClassType = typeof(OneMain),
                MyGlyph = "\uE80F",
                BgColor = new SolidColorBrush(Color.FromArgb(0xFF, 54, 195, 241))
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   文章",
                ClassType = typeof(Articles),
                MyGlyph = "\uE8C8",
                BgColor = new SolidColorBrush(Color.FromArgb(0xFF, 54, 195, 241))
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   问题",
                ClassType = typeof(OneQuestion),
                MyGlyph = "\uE7C5",
                BgColor = new SolidColorBrush(Color.FromArgb(0xFF, 54, 195, 241))
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   连载",
                ClassType = typeof(Serial),
                MyGlyph = "\uE8F7",
                BgColor = new SolidColorBrush(Color.FromArgb(0xFF, 54, 195, 241))
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   音乐",
                ClassType = typeof(Music),
                MyGlyph = "\uE995",
                BgColor = new SolidColorBrush(Color.FromArgb(0xFF, 54, 195, 241))
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   电影",
                ClassType = typeof(Movie),
                MyGlyph = "\uE714",
                BgColor = new SolidColorBrush(Color.FromArgb(0xFF, 54, 195, 241))
            });
        }
    }

}
