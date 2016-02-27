using Demo.BackGround;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace OtherTest
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
            var task = await TaskConfiguration.RegisterBackgroundTask(typeof(Demo.BackGround.LiveTileTask), "TestTaskName", new TimeTrigger(10000, false), null);
            // await new MessageDialog("may complete").ShowAsync();
            task.Completed += Task_Completed;
        }

        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            Debug.WriteLine("Complete..........");
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {

#if DEBUG
            this.DebugSettings.EnableFrameRateCounter |= System.Diagnostics.Debugger.IsAttached;
#endif
            //if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            //{
            //    bool loadState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
            //    ExtendedSplash extendedSplash = new ExtendedSplash(args.SplashScreen, loadState);
            //    Window.Current.Content = extendedSplash;
            //}
            //await ReisterBackground();
            //Window.Current.Activate();

            //var page = Window.Current.Content as Page;
            //if (page == null)
            //{
            //    SharePage sharepage = new SharePage();
            //    if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            //    {
            //        //TODO: Load state from previously suspended application
            //    }
            //    Window.Current.Content = sharepage;
            //}
            //Window.Current.Activate();

            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                if (rootFrame.ContentTransitions == null)
                    rootFrame.ContentTransitions = new TransitionCollection() { new NavigationThemeTransition() };

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
                Window.Current.Content = rootFrame;
                await ReisterBackground();
            }
            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(RequestLogIn), args.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

        }
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)


        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}

