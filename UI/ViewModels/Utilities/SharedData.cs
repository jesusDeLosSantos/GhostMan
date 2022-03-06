using BL.query;
using Entities;
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
        private static List<clsElementMap> mapSelectedToPlay;
        private static bool finPartida = false;
        public SharedData()
        {
            //todo: TRYCATCH!!!!!!!!!!!
            
        }
        //TODO: borrar el constructor, añadir set, y desde el constrcutor de custommodevm asignar el valor desde el set, y el converter edeberia poder acceder a ael
        public static List<ImageSource> AllImageSourceOfSprites { get => allImageSourceOfSprites; set => allImageSourceOfSprites = value; }

        public static List<clsElementMap> MapSelectedToPlay { get => mapSelectedToPlay; set => mapSelectedToPlay = value; }
        public static bool FinPartida { get => finPartida; set => finPartida = value; }
    }
}
