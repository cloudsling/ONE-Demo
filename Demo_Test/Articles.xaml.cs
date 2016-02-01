using System.ComponentModel;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
        public ArticlesObject DayArticlesObject { get; set; }

        public AritcalBinding ArticlesDataBinding { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Rect R = Window.Current.CoreWindow.Bounds;
            //ArticlesDataBinding.ScrollViewerHeight = Main.WindowHeight;
            ArticlesDataBinding.ArticalThemeColorModel = ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme);
            aaa.Text = "“ ";
            bbb.Text = " ”";
            InitializationSpeciallyString(600);
            ForBorder.Begin();
        }

        void InitializationSpeciallyString(int length)
        {
            if (length == 0) ArticlesDataBinding.FullContentBinding.FullContent = Main.dayReallyObject.Articles.Content;
            else {
                ArticlesDataBinding.FullContentBinding.FullContent = Main.dayReallyObject.Articles.Content.Substring(0, length) + "......";
            }
        }

        /// <summary>
        /// 初始化文章对象
        /// </summary>
        /// <param name="articlesObject"></param>
        void InitializationArticlesObject(ArticlesObject articlesObject)
        {
            DayArticlesObject.HeadContent = articlesObject.HeadContent;
            DayArticlesObject.Content = articlesObject.Content;
            DayArticlesObject.Header = articlesObject.Header;
            DayArticlesObject.Writer = articlesObject.Writer;
        }

        void LookMore_Click(object sender, RoutedEventArgs e)
        {
            LookMore.Visibility = Visibility.Collapsed;
            InitializationSpeciallyString(0);
        }
    }

    public class AritcalBinding
    {
        public AritcalBinding()
        {
            DayArticlesObject = new ArticlesObject();
            FullContentBinding = new SpeciallyString();
            InitializationArticlesObject(Main.dayReallyObject.Articles);
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

        void InitializationArticlesObject(ArticlesObject articlesObject)
        {
            DayArticlesObject.HeadContent = articlesObject.HeadContent;
            DayArticlesObject.Content = articlesObject.Content;
            DayArticlesObject.Header = articlesObject.Header;
            DayArticlesObject.Writer = articlesObject.Writer;
        }
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
