using System.ComponentModel;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Articles : Page
    {
        public Articles articles { get; set; }

        public Articles()
        {
            ArticlesDataBinding = new AritcalBinding();
            articles = this;
            this.InitializeComponent();
        }
        public ArticlesObject DayArticlesObject { get; set; }

        public AritcalBinding ArticlesDataBinding { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Window.Current.Content
            //double a = articles.ActualHeight;
            //Window.Current.Content.
            // Rect R = Window.Current.CoreWindow.Bounds;
            //ArticlesDataBinding.ScrollViewerHeight = Main.WindowHeight;
            aaa.Text = "“ ";
            bbb.Text = " ”";
            // InitializationArticlesObject(Main.dayReallyObject.Articles);
            InitializationSpeciallyString(500);
            ForBorder.Begin();
        }

        private void InitializationSpeciallyString(int length)
        {
            if (length == 0)
            {
                ArticlesDataBinding.FullContentBinding.FullContent = Main.dayReallyObject.Articles.Content;
            }
            else
            {
                ArticlesDataBinding.FullContentBinding.FullContent = Main.dayReallyObject.Articles.Content.Substring(0, length)+"......";
            }
        }
        /// <summary>
        /// 初始化文章对象
        /// </summary>
        /// <param name="articlesObject"></param>
        private void InitializationArticlesObject(ArticlesObject articlesObject)
        {
            DayArticlesObject.HeadContent = articlesObject.HeadContent;
            DayArticlesObject.Content = articlesObject.Content;
            DayArticlesObject.Header = articlesObject.Header;
            DayArticlesObject.Writer = articlesObject.Writer;
        }

        private void LookMore_Click(object sender, RoutedEventArgs e)
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

        private void InitializationArticlesObject(ArticlesObject articlesObject)
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
