using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UI.ViewModels.Utilities
{
    public class Utilidades
    {
        public static List<clsElementMap> ListaParedes { get; set; }//Necesario ponerlo aqui, porque la otra opcion seria al metodo mover de la clase enemigo se tendria que pasar por parametro la lista
        public static bool canMove(int x, int y)
        {
            bool result = true;
            clsElementMap placeholder;
            for (int i = 0; i < ListaParedes.Count && result; i++)
            {
                placeholder = ListaParedes.ElementAt(i);
                if (placeholder.AxisX == x && placeholder.AxisY == y)
                {
                    result = false;
                }
            }
            return result;
        }

        public static async void gameLost()
        {
            ContentDialog lost = new ContentDialog()
            {
                Title = "Has perdido.",
                Content = "Git gut.",
                PrimaryButtonText = "Aceptar"
            };

            ContentDialogResult result = await lost.ShowAsync();

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

        /// <summary>
        /// Cabecera: public async void mostrarMensajeAsync(string mensaje)
        /// Comentario: Este metodo se encarga de mostrar un MessageDialog con un mensaje que tendra una opcion de cerrar.
        /// Entradas: string mensaje
        /// Salidas: Ninguna
        /// Precondiciones: Ninguna
        /// Postcondiciones: Se mostrara un mensaje al usuario en un MessageDialog, que contentra una opcion de cerrar.
        /// </summary>
        /// <param name="mensaje"></param>
        public static async void mostrarMensajeAsync(string mensaje)
        {
            var dialog = new MessageDialog(mensaje);
            await dialog.ShowAsync();
        }
    }
}
