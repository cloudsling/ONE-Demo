﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using System.ComponentModel;
using System.Threading;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Windows.Storage;
using System.Runtime.CompilerServices;

namespace Demo
{
    public sealed partial class Main : Page
    {
        public static Main MainCurrent;

        public MainViewModel mainViewModel { get; set; }

        public static List<DayObject> DayObjectCollection;

        public static DayReallyObject dayReallyObject;

        public static Action CreateFileButtonClick;

        public static StorageFolder oneFolder;

        public Main()
        {
            mainViewModel = new MainViewModel();
            this.InitializeComponent();
            MainCurrent = this;
                SystemNavigationManager.GetForCurrentView().BackRequested += Main_BackRequested;
        }

        private async void Main_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (mainViewModel.oneSettings.IsDoubleClickExit)
            {
                if (OneFrame.CurrentSourcePageType == typeof(OneMain))
                {
                    e.Handled = true;
                    if (Math.Abs(mainViewModel.SomethingInMainSettings.NotifyUserWidth - 201) <= 1.0)
                    {
                        Application.Current.Exit();
                    }
                    NotifyUserMethod("再按一次后退键退出程序", 201);
                    await Task.Delay(2000);
                    mainViewModel.SomethingInMainSettings.NotifyUserWidth = 180;
                    return;
                }
            }
            if (OneFrame.CanGoBack)
            {
                OneFrame.GoBack();
                if (OneFrame.CurrentSourcePageType == typeof(BlankPage))
                {
                    OneFrame.GoBack();
                }
                e.Handled = true;
            }
        }

        public static string itemPath;
        public static HttpClient httpClient;
        public static string x;
        public static List<string> imageSourceAsync;
        public static string uri = "http://wufazhuce.com/";
        public static string uriOneObject = "http://wufazhuce.com/one/";
        public static string dayReallyObjectString;

        private async static void GetOneString(string uri)
        {
            httpClient = new HttpClient();
            x = await httpClient.GetStringAsync(new Uri(uri));
        }

        public async static Task<string> GetDayReallyObjectString(string vol)
        {
            if (httpClient != null) httpClient.Dispose();
            httpClient = new HttpClient();
            return await httpClient.GetStringAsync(new Uri(uriOneObject + vol));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((e.Parameter as string) == "404")
            {
                HEADER111.Text = "关于";
                if (isHide.Visibility == Visibility.Visible)
                {
                    isHide.Visibility = Visibility.Collapsed;
                }
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
                bar.IsIndeterminate = false;
                ring.IsActive = false;
                OneFrame.Navigate(typeof(OneMain));
                StorageFolder storageFolder = KnownFolders.PicturesLibrary;
                oneFolder = await storageFolder.CreateFolderAsync("ONE-一个", CreationCollisionOption.OpenIfExists);
            }

            mainViewModel.oneSettings.GiveMeGood += 1;
            if (mainViewModel.oneSettings.GiveMeGood == 10)
            {
                if (cb.IsChecked == false)
                {
                    mainViewModel.oneSettings.GiveMeGood = 0;
                }
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    await StatusBar.GetForCurrentView().HideAsync();
                }
                StarStar.Visibility = Visibility.Visible;
                GiveMeStar.Begin();
            }
        }

        /// <summary>
        /// 刷新首页
        /// </summary>
        public async static void Refreshen()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar.GetForCurrentView().ProgressIndicator.Text = "正在刷新";
                StatusBar.GetForCurrentView().ProgressIndicator.ProgressValue = null;
                await StatusBar.GetForCurrentView().ProgressIndicator.ShowAsync();
            }
            //await what.ShowAsync();
            //AppBar.IsOpen = false;
            var currentFrame = MainCurrent.OneFrame.CurrentSourcePageType;
            MainCurrent.OneFrame.Navigate(typeof(BlankPage));
            try
            {
                //GetOneString(uri);
                //DayObjectCollection = GetOne.GetOneTodayObjectList(x);
            }
            catch (Exception)
            {
                await new MessageDialog("opps！请检查宁德网络连接，离线版本将在后续版本开发o((⊙﹏⊙))o.").ShowAsync();
                NotifyUserMethod("刷新失败！", 180);
            }
            MainCurrent.OneFrame.Navigate(currentFrame);

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar.GetForCurrentView().ProgressIndicator.Text = "Many-多个";
                StatusBar.GetForCurrentView().ProgressIndicator.ProgressValue = 0;
                await StatusBar.GetForCurrentView().ProgressIndicator.ShowAsync();
            }

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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Refreshen();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Visibility = Visibility.Collapsed;
            isHide.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            isHide.Visibility = Visibility.Collapsed;
            txtSearch.Visibility = Visibility.Visible;
            txt_Search.Focus(FocusState.Keyboard);
        }

        private void AppBar_LostFocus(object sender, RoutedEventArgs e)
        {
            this.AppBar.IsOpen = false;
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreateFileButtonClick != null) CreateFileButtonClick();
            this.AppBar.IsOpen = false;
        }

        private void HamBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchSplitter();
        }
        /// <summary>
        /// 关闭或者呼出汉堡菜单
        /// </summary>
        private void SwitchSplitter()
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }

        public void ChangeIsHide()
        {
            if (isHide.Visibility != Visibility.Collapsed)
            {
                isHide.Visibility = Visibility.Visible;
            }
            else
            {
                isHide.Visibility = Visibility.Collapsed;
            }
        }

        private async void Love()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9NBLGGH58VLR"));
        }
        /// <summary>
        /// 算是导航了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HamListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView oneListView = sender as ListView;
            switch (oneListView.Name)
            {
                case "HamListBox":
                    {
                        MyListViewItems m = oneListView.SelectedItem as MyListViewItems;
                        if (m != null)
                        {
                            HEADER111.Text = "ONE- " + m.MyItemName.Trim();
                            if (isHide.Visibility == Visibility.Collapsed) isHide.Visibility = Visibility.Visible;
                            if (AppBar.Visibility == Visibility.Collapsed) AppBar.Visibility = Visibility.Visible;
                            OneFrame.Navigate(m.ClassType);
                            if (OneFrame.CurrentSourcePageType == typeof(Articles) || OneFrame.CurrentSourcePageType == typeof(OneQuestion))
                            {
                                CreateFileButton.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                CreateFileButton.Visibility = Visibility.Visible;
                            }
                            NotifyUserMethod("ONE-" + m.MyItemName, 180);
                            if (oneListView.SelectedItem == null)
                            {
                                return;
                            }
                            oneListView.SelectedItem = null;
                            Splitter.IsPaneOpen = false;
                        }                    }
                    break;
                case "Settings":
                    {
                        if (oneListView.SelectedItem == null)
                        {
                            return;
                        }
                        oneListView.SelectedItem = null;
                        SwitchSplitter();
                        AppBar.Visibility = Visibility.Collapsed;
                        HEADER111.Text = "设置";
                        if (isHide.Visibility == Visibility.Visible)
                        {
                            isHide.Visibility = Visibility.Collapsed;
                        }
                        OneFrame.Navigate(typeof(Settings));
                    }
                    break;
                case "Others":
                    {
                        if (oneListView.SelectedItem == null)
                        {
                            return;
                        }
                        SwitchSplitter();
                        int id = Others.SelectedIndex;
                        switch (id)
                        {
                            case 0:
                                Love(); break;
                            case 1:
                                {
                                    //balabala.Visibility = Visibility.Collapsed;
                                    HEADER111.Text = "关于";
                                    AppBar.Visibility = Visibility.Collapsed;
                                    if (isHide.Visibility == Visibility.Visible) { isHide.Visibility = Visibility.Collapsed; }
                                    OneFrame.Navigate(typeof(About));
                                }
                                break;
                        }
                        oneListView.SelectedItem = null;
                    }
                    break;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GiveMeStarOk.Begin();
            await Task.Delay(650);
            StarStar.Visibility = Visibility.Collapsed;
            Love();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().ShowAsync();
            }
        }

        private void DemoDemo_ManipulationCompleted(object sender, Windows.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs e)
        {
            SwitchSplitter();
        }
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
        private string _MyitemName;
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
        private string _MyGlyph;
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
                MyGlyph = "\uE80F"
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   文章",
                ClassType = typeof(Articles),
                MyGlyph = "\uE8C8"
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   问题",
                ClassType = typeof(OneQuestion),
                MyGlyph = "\uE7C5"
            });
            this.Add(new MyListViewItems
            {
                MyItemName = "   东西",
                ClassType = typeof(OneThing),
                MyGlyph = "\uE8A4"
            });
        }
    }

}
