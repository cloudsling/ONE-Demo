using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace OtherTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharePage : Page
    {
        public SharePage CurrentSharePage { get; set; }
        double xb;
        double yb;
        double WindowSizeX;
        double WindowSizeY;
        public SharePage()
        {
            this.InitializeComponent();
            CurrentSharePage = this;
            yb = (double)rec.GetValue(Canvas.TopProperty);
            xb = (double)rec.GetValue(Canvas.LeftProperty);
            WindowSizeX = Window.Current.Bounds.Width - rec.Width;
            WindowSizeY = Window.Current.Bounds.Height - rec.Height;
        }

        private void Rectangle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            yb = (double)rec.GetValue(Canvas.TopProperty);
            xb = (double)rec.GetValue(Canvas.LeftProperty);
            var x = e.Cumulative.Translation.X;
            var y = e.Cumulative.Translation.Y;
        }

        private void rec_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var tempX = e.Cumulative.Translation.X;
            var tempY = e.Cumulative.Translation.Y;
            if (tempX + xb < WindowSizeX)
            {
                rec.SetValue(Canvas.LeftProperty, (xb + tempX));
            }
            else
            {
                var buf = xb + tempX;
                while (buf >= WindowSizeX)
                {
                    buf -= WindowSizeX;
                }
                rec.SetValue(Canvas.LeftProperty, buf);
            }
            if (tempY + yb < WindowSizeY)
            {
                rec.SetValue(Canvas.TopProperty, (yb + tempY));
            }
            else
            {
                var buf = yb + tempY;
                while (buf >= WindowSizeY)
                {
                    buf -= WindowSizeY;
                }
                rec.SetValue(Canvas.TopProperty, buf);

            }

        }

        private double GetTransX(double x)
        {

            return 0;
        }


        async void Button_Click(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("测试哦", "温馨提示：").ShowAsync();
        }
    }
}
