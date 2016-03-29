using JYAnalyticsUniversal;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    sealed partial class App : Application
    {
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        async Task ReisterBackground()
        {
            var task = await TaskConfiguration.RegisterBackgroundTask(typeof(BackGround.LiveTileTask), "LiveTileTask", new TimeTrigger(60 * 12, false), new SystemCondition(SystemConditionType.InternetAvailable));

            // await new MessageDialog("may complete").ShowAsync();
            task.Completed += Task_Completed;
        }

        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            Debug.WriteLine("Complete..........");
        }

        protected async override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
            await JYAnalytics.StartTrackAsync("e3c602f86e5e70b351479eddf2d1efc8");
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            await JYAnalytics.StartTrackAsync("e3c602f86e5e70b351479eddf2d1efc8");
            AdDuplex.AdDuplexClient.Initialize("aaa53830-3488-4201-8d47-c9a9395dab99");
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                rootFrame.Navigated += RootFrame_Navigated;
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
                await ReisterBackground();
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(StartPage), e.Arguments);
            }
            // 确保当前窗口处于活动状态
            Window.Current.Activate();

        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = (Window.Current.Content as Frame).BackStack.Any() ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动 
            await JYAnalytics.EndTrackAsync();
            deferral.Complete();
        }
    }
}
