using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UI.ViewModels.Utilities.Converters
{
    class ConverterIdElementToSourceImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            String imagen="";
            switch (value)
            {
                case 1:
                    imagen = "/Assets/Prueba/Corner_Wall_Test.png";
                    break;
                case 2:
                    imagen = "/Assets/Prueba/Corner_Wall_Test_2.png";
                    break;
                case 3:
                    imagen = "/Assets/Prueba/Corner_Wall_Test_3.png";
                    break;
                case 4:
                    imagen = "/Assets/Prueba/Horizontal_Wall_Test.png";
                    break;
            }
            return imagen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
