using BL.query;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UI.ViewModels.Utilities.Converters
{
    public class ConverterIdElementToSourceImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //TODO: ¡¡¡MUY IMPORTANTE!!! MIRAR COMO OPTIMIZAR ESTO
            //ImageSource imageSource = convertirByteImagen(clsElementTypeQueryBL.getSpriteOfElementTypeIdBL((int)value));
            List<ImageSource> allImageSprites = SharedData.AllImageSourceOfSprites;
            
            return allImageSprites[(int)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
