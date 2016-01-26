using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    public sealed partial class OneQuestion : Page
    {
        public OneQuestion()
        {
            OneQuestionObjectBinding = new OneQuestionDataBinding();
            InitializeComponent();
            Main.OtherPageDoWhenThemeChanged = () => ThemeColorModel.InitialByOtherObject(OneQuestionObjectBinding.OneQuestionThemeColorModel, ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme));
        }

        public OneQuestionDataBinding OneQuestionObjectBinding { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            OneQuestionObjectBinding.OneQuestionThemeColorModel = ThemeColorModel.GetTheme(Main.MainCurrent.mainViewModel.oneSettings.RequireLightTheme);
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

        public ThemeColorModel OneQuestionThemeColorModel { get; set; }

        void InitializeOneQuestionObjectBinding(OneQuestionObject oneQuestionObject)
        {
            OneQuestionObject.AskerName = oneQuestionObject.AskerName;
            OneQuestionObject.AskContent = oneQuestionObject.AskContent;
            OneQuestionObject.AnswerContent = oneQuestionObject.AnswerContent;
            StringBuilder sb = new StringBuilder(oneQuestionObject.AnswerName);
            for (int i = oneQuestionObject.AnswerName.Length - 1; i > 0; i--)
                sb.Insert(i, "\r\n");
            OneQuestionObject.AnswerName = sb.ToString().Trim();
        }
    }

}
