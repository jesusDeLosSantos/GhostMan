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
            ImageSource imageSource = convertirByteImagen(clsElementTypeQueryBL.getSpriteOfElementTypeIdBL((int)value));
            
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Este metodo se encarga de convertir una array de bytes a imagen
        /// </summary>
        private ImageSource convertirByteImagen(byte[] sprite)
        {
            BitmapImage imagenBitMap = new BitmapImage();
            if (sprite != null)
            {
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    var result = new BitmapImage();
                    stream.WriteAsync(sprite.AsBuffer());
                    stream.Seek(0);
                    imagenBitMap.SetSource(stream);
                }
            }
            return imagenBitMap;
        }
    }
}
