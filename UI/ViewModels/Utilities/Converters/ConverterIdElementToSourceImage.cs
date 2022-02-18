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
                case 0:
                    imagen = "/Assets/Prueba/Empty_Spot.png";
                    break;
                case 1:
                    imagen = "/Assets/Prueba/Corner_Wall_1.png";
                    break;
                case 2:
                    imagen = "/Assets/Prueba/Corner_Wall_2.png";
                    break;
                case 3:
                    imagen = "/Assets/Prueba/Corner_Wall_3.png";
                    break;
                case 4:
                    imagen = "/Assets/Prueba/Corner_Wall_4.png";
                    break;
                case 5:
                    imagen = "/Assets/Prueba/Cross_Wall.png";
                    break;
                case 6:
                    imagen = "/Assets/Prueba/Ending_Wall_1.png";
                    break;
                case 7:
                    imagen = "/Assets/Prueba/Ending_Wall_2.png";
                    break;
                case 8:
                    imagen = "/Assets/Prueba/Ending_Wall_3.png";
                    break;
                case 9:
                    imagen = "/Assets/Prueba/Ending_Wall_4.png";
                    break;
                case 10:
                    imagen = "/Assets/Prueba/Horizontal_Wall.png";
                    break;
                case 11:
                    imagen = "/Assets/Prueba/Section_Wall_1.png";
                    break;
                case 12:
                    imagen = "/Assets/Prueba/Section_Wall_2.png";
                    break;
                case 13:
                    imagen = "/Assets/Prueba/Section_Wall_3.png";
                    break;
                case 14:
                    imagen = "/Assets/Prueba/Section_Wall_4.png";
                    break;
                case 15:
                    imagen = "/Assets/Prueba/Vertical_Wall.png";
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
