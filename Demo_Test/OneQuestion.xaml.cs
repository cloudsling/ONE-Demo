using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class OneQuestion : Page
    {
        public OneQuestion()
        {
            OneQuestionObjectBinding = new OneQuestionDataBinding();
            this.InitializeComponent();
        }

        public OneQuestionDataBinding OneQuestionObjectBinding { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //InitializeOneQuestionObjectBinding(Main.dayReallyObject.OneQuestion);
            borderAnimeStoryBoard.Begin();
        }
        

      
    }

    public class OneQuestionDataBinding
    {
        public OneQuestionDataBinding()
        {
            OneQuestionObject = new OneQuestionObject();
            InitializeOneQuestionObjectBinding(Main.dayReallyObject.OneQuestion);
        }

        public OneQuestionObject OneQuestionObject { get; set; }

        private void InitializeOneQuestionObjectBinding(OneQuestionObject oneQuestionObject)
        {
            OneQuestionObject.AskerName = oneQuestionObject.AskerName;
            OneQuestionObject.AskContent = oneQuestionObject.AskContent;
            OneQuestionObject.AnswerContent = oneQuestionObject.AnswerContent;
            StringBuilder sb = new StringBuilder(oneQuestionObject.AnswerName);
            for (int i = oneQuestionObject.AnswerName.Length - 1; i > 0; i--)
            {
                sb.Insert(i, "\r\n");
            }
            OneQuestionObject.AnswerName = sb.ToString().Trim();
        }
    }

}
