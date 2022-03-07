using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UI.ViewModels.Utilities
{
    public class fromByteToImageConverter
    {
        public static List<ImageSource> convertirByteImagen(List<byte[]> sprites)
        {
            List<ImageSource> imageSources = new List<ImageSource>();
            if (sprites != null)
            {
                foreach (var sprite in sprites)
                {
                    using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                    {

                        BitmapImage imagenBitMap = new BitmapImage();
                        var result = new BitmapImage();
                        stream.WriteAsync(sprite.AsBuffer());
                        stream.Seek(0);
                        imagenBitMap.SetSource(stream);
                        imageSources.Add(imagenBitMap);
                    }
                }
            }
            return imageSources;
        }
    }
}
