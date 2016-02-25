using Demo.http;
using Demo.Models;
using JYAnalyticsUniversal;
using System;
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            OneQuestionObjectBinding.OneQuestionThemeColorModel = Main.MainCurrent.mainViewModel.themeColorModelSettings;
            if (e.Parameter != null)
            {
                var temp = e.Parameter as ReadingModel;
                if (temp != null)
                {
                    using (var client = HttpHelper.CreateHttpClientWithUserAgent())
                    {
                        var response = await client.GetStringAsync(new Uri("http://wufazhuce.com/question/" + temp.ID.ToString()));
                        InitializeOneQuestionObjectBinding(GetOne.GetOneQuestionObject(response));
                    }
                }
            }
            else
            {
                InitializeOneQuestionObjectBinding(Main.dayReallyObject.OneQuestion);
            }
            borderAnimeStoryBoard.Begin();
            JYAnalytics.TrackPageStart("question_page");
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            JYAnalytics.TrackPageEnd("question_page");
        }
        void InitializeOneQuestionObjectBinding(OneQuestionObject oneQuestionObject)
        {
            OneQuestionObjectBinding.OneQuestionObject.AskerName = oneQuestionObject.AskerName;
            OneQuestionObjectBinding.OneQuestionObject.AskContent = oneQuestionObject.AskContent;
            OneQuestionObjectBinding.OneQuestionObject.AnswerContent = oneQuestionObject.AnswerContent;
            StringBuilder sb = new StringBuilder(oneQuestionObject.AnswerName);
            for (int i = oneQuestionObject.AnswerName.Length - 1; i > 0; i--)
                sb.Insert(i, "\r\n");
            OneQuestionObjectBinding.OneQuestionObject.AnswerName = sb.ToString().Trim();
        }
    }

    public class OneQuestionDataBinding
    {
        public OneQuestionDataBinding()
        {
            OneQuestionObject = new OneQuestionObject();
        }

        public OneQuestionObject OneQuestionObject { get; set; }

        public ThemeColorModel OneQuestionThemeColorModel { get; set; }


    }

}
