using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UI.ViewModels.Utilities.Converters
{
    public class ConverterSizeToWidth : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int width; 
            switch (value)
            {
                case 16:
                    width = 800;
                break;

                case 24:
                    width  = 1200;
                break;

                default: //Por defecto se pone el tamaño maximo
                    width = 1500;
               break;

            }

            SharedData.MaxMapWidth = width;//Se guarda el sharedData para que el enemigo y el jugador puedan saber cual es el maximo 
            return width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
