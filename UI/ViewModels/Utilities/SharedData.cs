using BL.query;
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
    public class SharedData
    {
        private static List<ImageSource> allImageSourceOfSprites;
        public SharedData()
        {
            //todo: TRYCATCH!!!!!!!!!!!
            
        }
        //TODO: borrar el constructor, añadir set, y desde el constrcutor de custommodevm asignar el valor desde el set, y el converter edeberia poder acceder a ael
        public static List<ImageSource> AllImageSourceOfSprites { get => allImageSourceOfSprites; set => allImageSourceOfSprites = value; }

        
    }
}
