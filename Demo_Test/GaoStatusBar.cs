using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace Demo
{
    static class GaoStatusBar
    {
        static StatusBar statusBar = StatusBar.GetForCurrentView();
        static StatusBarProgressIndicator progressIndicator = StatusBar.GetForCurrentView().ProgressIndicator;
        /// <summary>
        /// if we can modify StatusBar
        /// </summary>
        /// <returns></returns>
        static readonly bool IfCanModifyStatusBar = Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar");
        /// <summary>
        /// Hide StatusBar if can do
        /// </summary>
        public async static void HideStatusBar()
        {
            if (IfCanModifyStatusBar) await statusBar.HideAsync();
        }
        /// <summary>
        /// Show StatusBar
        /// </summary>
        public async static void ShowStatusBar()
        {
            if (IfCanModifyStatusBar) await statusBar.ShowAsync();
        }
        /// <summary>
        /// set StatusBar
        /// </summary>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="backgroundOpacity"></param>
        public static void SetStatusBar(Color foregroundColor, Color backgroundColor, double backgroundOpacity = 1)
        {
            statusBar.ForegroundColor = foregroundColor;
            statusBar.BackgroundColor = backgroundColor;
            statusBar.BackgroundOpacity = backgroundOpacity;
            ShowStatusBar();
        }

        public async static void SetStatusBarProgressIndicator(double? progressValue, string text = "Many-多个")
        {
            if (!IfCanModifyStatusBar) return;
            progressIndicator.ProgressValue = progressValue;
            progressIndicator.Text = text;
            await progressIndicator.ShowAsync();
        }
    }
}
