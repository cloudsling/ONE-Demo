using Demo;
using Demo.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Demo.ViewModel
{
    public class SearchPageViewModel
    {
        public SearchPageViewModel()
        {
            DayObject = new ObservableCollection<DayObject>();
            for (int i = 0; i < 5; i++)
            {
                DayObject.Add(new DayObject
                {
                    ByWho = "adafbafdbsfd 作品",
                    MainString = "bbabababbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
                    DayImagePath = "ms-appx:///Assets/SunOrNightMode/night1.png"
                });
            }
#if DEBUG
            //if (DesignMode.DesignModeEnabled)
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        DayObject.Add(new DayObject
            //        {
            //            ByWho = "adafbafdbsfd 作品",
            //            MainString = "bbabababbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
            //            DayImagePath = "Assets/SunOrNightMode/night1.png"
            //        });
            //    }
            //}
#endif
        }

        public ObservableCollection<DayObject> DayObject { get; set; }

    }
}
