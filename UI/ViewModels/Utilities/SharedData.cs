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
        private static bool isCommunityMap = true;
        private static bool finPartida = false;

        public static List<ImageSource> AllImageSourceOfSprites { get => allImageSourceOfSprites; set => allImageSourceOfSprites = value; }

        public static List<clsElementMap> MapSelectedToPlay { get => mapSelectedToPlay; set => mapSelectedToPlay = value; }

        public static bool IsCommunityMap { get => isCommunityMap; set => isCommunityMap = value; }

        public static bool FinPartida { get => finPartida; set => finPartida = value; }
    }
}
