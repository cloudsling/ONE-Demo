using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OtherTest
{
    public sealed partial class MainPage : Page
    {

        public MainPage_ViewModel MainPageViewModel { get; set; }

        public MainPage()
        {
            MainPageViewModel = new MainPage_ViewModel { ColorTestModel = new ColorTest() };
            this.InitializeComponent();
            MainPageViewModel.ColorTestModel.BackGColor = new SolidColorBrush(Color.FromArgb(0xff, 249, 38, 114));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FlipView fv = new FlipView();

        }


    }

    public class MainPage_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string PropName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(PropName));
        }
        private ColorTest _colorTestModel;

        public ColorTest ColorTestModel
        {
            get { return _colorTestModel; }
            set
            {
                _colorTestModel = value;
                OnPropertyChanged();
            }
        }

    }
}
