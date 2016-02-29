using System;
using Demo.http;
using Demo.Models;
using JYAnalyticsUniversal;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using System.Threading.Tasks;

namespace Demo
{
    public sealed partial class Articles : Page
    {
        public Articles()
        {
            ArticlesDataBinding = new AritcalBinding();
            InitializeComponent();
            Main.OtherPageDoWhenThemeChanged = () => ThemeColorModel.InitialByOtherObject(ArticlesDataBinding.ArticalThemeColorModel, ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme));
        }
        // public ArticlesObject DayArticlesObject { get; set; }

        public AritcalBinding ArticlesDataBinding { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Rect R = Window.Current.CoreWindow.Bounds;
            //ArticlesDataBinding.ScrollViewerHeight = Main.WindowHeight;
            ArticlesDataBinding.ArticalThemeColorModel = ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme);
            aaa.Text = "“ ";
            bbb.Text = " ”";
            ForBorder.Begin();
            if (e.Parameter != null)
            {
                var temp = e.Parameter as ReadingModel;
                if (temp != null)
                {
                    using (var client = HttpHelper.CreateHttpClientWithUserAgent())
                    {
                        var response = await client.GetStringAsync(new Uri("http://wufazhuce.com/article/" + temp.ID.ToString()));
                        InitializationArticlesObject(GetOne.GetArticlesObject(response));
                        InitializationSpeciallyString(600, ArticlesDataBinding.DayArticlesObject.Content);
                    }
                }
            }
            else
            {
                //while (string.IsNullOrEmpty(ArticlesDataBinding.DayArticlesObject.HeadContent))
                //{
                //    Loading.IsIndeterminate = true;
                //    await Task.Delay(10);
                //}
                Loading.IsIndeterminate = false;
                InitializationArticlesObject(Main.dayReallyObject.Articles);
                InitializationSpeciallyString(600, Main.dayReallyObject.Articles.Content);
            }
            JYAnalytics.TrackPageStart("article_page");
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            JYAnalytics.TrackPageEnd("article_page");
        }

        void InitializationSpeciallyString(int length, string content)
        {
            if (length == 0) ArticlesDataBinding.FullContentBinding.FullContent = content;
            else {
                ArticlesDataBinding.FullContentBinding.FullContent = content.Substring(0, length) + "......";
            }
        }

        /// <summary>
        /// 初始化文章对象
        /// </summary>
        /// <param name="articlesObject"></param>
        void InitializationArticlesObject(ArticlesObject articlesObject)
        {
            ArticlesDataBinding.DayArticlesObject.HeadContent = articlesObject.HeadContent;
            ArticlesDataBinding.DayArticlesObject.Header = articlesObject.Header;
            ArticlesDataBinding.DayArticlesObject.Writer = articlesObject.Writer;
            ArticlesDataBinding.DayArticlesObject.Content = articlesObject.Content;
        }

        void LookMore_Click(object sender, RoutedEventArgs e)
        {
            LookMore.Visibility = Visibility.Collapsed;
            InitializationSpeciallyString(0, ArticlesDataBinding.DayArticlesObject.Content);
        }
    }

    public class AritcalBinding
    {
        public AritcalBinding()
        {
            try
            {
                DayArticlesObject = new ArticlesObject();
                FullContentBinding = new SpeciallyString();
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message + "--------------");
            }
        }
        public SpeciallyString FullContentBinding { get; set; }

        public ArticlesObject DayArticlesObject { get; set; }

        // public ThemeColorModel articalThemeColorModel { get; set; }

        public ThemeColorModel ArticalThemeColorModel
        {
            get
            {
                return _articalThemeColorModel;
            }

            set
            {
                _articalThemeColorModel = value;
            }
        }

        ThemeColorModel _articalThemeColorModel;


    }

    public class SpeciallyString : INotifyPropertyChanged
    {
        private string _fullContent;

        public string FullContent
        {
            get { return _fullContent; }
            set
            {
                _fullContent = value;
                PropertyChangedEventHandler temp = Volatile.Read(ref PropertyChanged);
                if (temp != null) temp(this, new PropertyChangedEventArgs("FullContent"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
