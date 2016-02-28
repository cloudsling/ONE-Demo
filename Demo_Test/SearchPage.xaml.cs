using Demo.Models;
using Demo.ViewModel;
using JYAnalyticsUniversal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    /// <summary>
    /// auto event
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            JYAnalytics.TrackPageStart("search_page");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            JYAnalytics.TrackPageEnd("search_page");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush selected = new SolidColorBrush(Color.FromArgb(0xFF, 255, 12, 12));
            SolidColorBrush unselect = new SolidColorBrush(Color.FromArgb(0xFF, 54, 195, 241));
            var btn = sender as Button;
            var grid = btn.Parent as Grid;
            if (grid != null)
            {
                foreach (var item in grid.Children)
                {
                    var temp = item as Button;
                    if (temp == null) continue;
                    if (temp.Tag.ToString() == btn.Tag.ToString())
                    {
                        temp.Foreground = selected;
                    }
                    else
                    {
                        temp.Foreground = unselect;
                    }
                }
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var what = e.ClickedItem;
            var t = e.ClickedItem.GetType();
            switch (t.Name)
            {
                case nameof(DayObject):
                    this.Frame.Navigate(typeof(OneMain), what);
                    break;
                case nameof(ReadingModel):
                    this.Frame.Navigate((what as ReadingModel).PageType, what);
                    break;
                case nameof(MusicModel):
                    this.Frame.Navigate(typeof(Music), what);
                    break;
                case nameof(MovieModel):
                    this.Frame.Navigate(typeof(MovieDetile), "http://m.wufazhuce.com/movie/" + (what as MovieModel).Id.ToString());
                    break;
            }
        }
    }

    /// <summary>
    /// entry and method
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            SearchPageViewModel = new SearchPageViewModel();
            this.InitializeComponent();
            searchPageCurrent = this;
            if (Main.MainCurrent.mainViewModel.oneSettings.SearchCacheMode)
            {
                this.NavigationCacheMode = NavigationCacheMode.Required;
            }
            SearchPageViewModel.ThemeColor = Main.MainCurrent.mainViewModel.themeColorModelSettings;
            Main.OtherPageDoWhenThemeChanged = () => ThemeColorModel.InitialByOtherObject(SearchPageViewModel.ThemeColor, Main.MainCurrent.mainViewModel.themeColorModelSettings);
        }

        public async Task SearchStart(string searchContent)
        {
            searchStartAnimation.Begin();
            Button_Click(def, new RoutedEventArgs());
            SearchPageViewModel.SearchContent = searchContent;
            await SearchPageViewModel.SearchMain();
        }
    }

    /// <summary>
    /// field and property
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public static SearchPage searchPageCurrent;
        public SearchPageViewModel SearchPageViewModel { get; set; }
    }
}
