using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Email;

namespace Demo
{
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Story.Begin();
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var emailMessage = new EmailMessage();
            emailMessage.Subject = "反馈";
            emailMessage.Body = "";
            emailMessage.To.Add(new EmailRecipient("lingmengc@outlook.com"));
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }
    }
    
}
