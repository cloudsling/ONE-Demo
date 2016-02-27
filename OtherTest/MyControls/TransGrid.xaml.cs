using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OtherTest.MyControls
{
    public sealed partial class TransGrid : UserControl
    {
        double xb;
        double yb;
        double WindowSizeX;
        double WindowSizeY;

        public TransGrid()
        {
            this.InitializeComponent();
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
    }
}
