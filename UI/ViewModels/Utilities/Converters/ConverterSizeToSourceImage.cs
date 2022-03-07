using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UI.ViewModels.Utilities.Converters
{
    public class ConverterSizeToSourceImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string source = ""; 
            switch (value) {
                case 16:
                    source = "/Assets/Image/Easy_Mode.png";
                    break;

                case 24:
                    source = "/Assets/Image/Medium_Mode.png";
                    break;

                case 30:
                    source = "/Assets/Image/Hard_Mode.png";
                    break;
            }

            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
