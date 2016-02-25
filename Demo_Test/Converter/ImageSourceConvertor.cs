using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Demo.Converter
{
    class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var temp = value as Type;
            if (temp == typeof(Articles))
            {
                return "ms-appx:///Assets/Res/s_artcile.png";
            }
            if (temp == typeof(OneQuestion))
            {
                return "ms-appx:///Assets/Res/qa_image.png";
            }
            if (temp == typeof(Serial))
            {
                return "ms-appx:///Assets/Res/serial_image.png";
            }
            return "ms-appx:///Assets/Res/s_artcile.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0;
        }
    }
}
