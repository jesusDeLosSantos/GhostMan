using BL.query;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UI.ViewModels.Utilities
{
    public class SharedData
    {
        private static List<ImageSource> allImageSourceOfSprites;
        private static List<clsElementMap> mapSelectedToPlay;
        private static bool isCommunityMap = true;
        private static string completionTime = "";
        private static bool finPartida = false;

        public static List<ImageSource> AllImageSourceOfSprites { get => allImageSourceOfSprites; set => allImageSourceOfSprites = value; }

        public static List<clsElementMap> MapSelectedToPlay { get => mapSelectedToPlay; set => mapSelectedToPlay = value; }

        public static bool IsCommunityMap { get => isCommunityMap; set => isCommunityMap = value; }

        public static bool FinPartida { get => finPartida; set => finPartida = value; }

        public static string CompletionTime { get => completionTime; set => completionTime = value; }
        public static async void gameLost()
        {
            ContentDialog lost = new ContentDialog()
            {
                Title = "Has perdido.",
                Content = "Git gut.",
                PrimaryButtonText = "Aceptar"
            };

            ContentDialogResult result = await lost.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                (Window.Current.Content as Frame).Navigate(typeof(MainPage));
            }
        }

        public static async Task<String> gameWon()
        {
            TextBox inputTextBox = new TextBox();
            ContentDialog won = new ContentDialog()
            {
                Title = "¡Has ganado! Introduce tu nick.",
                Content = inputTextBox,
                PrimaryButtonText = "Aceptar"
            };

            ContentDialogResult result = await won.ShowAsync();
            (Window.Current.Content as Frame).Navigate(typeof(MainPage));
            return inputTextBox.Text;

            
        }
    }
}
