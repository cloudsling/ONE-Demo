﻿using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    public sealed partial class StartMainPage : Page
    {
        public static StartMainPage CurrentPage;

        public DayObject PageDataBinding { get; set; }

        public StartMainPage()
        {
            PageDataBinding = new DayObject();
            try
            {
                StatusBar.GetForCurrentView().HideAsync().AsTask();
            }
            catch (Exception)
            {
            }
            this.InitializeComponent();
            CurrentPage = this;
        }

        public List<DayObject> dayObjectCollection;

        public static MyNavigationEventArgs sth;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            sth = e.Parameter as MyNavigationEventArgs;
            ChangeMainCurrentMsg(sth.dayObjectCollection[0]);
            BigBig.Begin();
        }

        public async void ChangeMainCurrentMsg(DayObject obj)
        {
            PageDataBinding.ByWho = obj.ByWho;
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.GetFileAsync("StartMainPage.jpg");
            string path = file.Path;
            PageDataBinding.DayImagePath = path;
            PageDataBinding.HeaderString = obj.HeaderString;
            PageDataBinding.MainString = obj.MainString;
            PageDataBinding.Vol = obj.Vol;
            PageDataBinding.OneDay = obj.OneDay;
            PageDataBinding.OneMonthAndYear = obj.OneMonthAndYear;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Main), sth);
            try
            {
                var bar = StatusBar.GetForCurrentView();
                var what = bar.ProgressIndicator;
                await what.ShowAsync();
                bar.ForegroundColor = Colors.White;
                bar.BackgroundColor = Colors.DeepSkyBlue;
                bar.BackgroundOpacity = 1;
                await bar.ShowAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}