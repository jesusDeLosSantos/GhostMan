using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UI.ViewModels.Utilities
{
    public abstract class clsVMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     <header>public async void showSuccess()</header>
        ///     <description>This method shows a message that indicates success</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Shows a success message</postcondition>
        /// </summary>
        public async void showSuccess()
        {
            ContentDialog datosEnviados = new ContentDialog
            {
                Title = "SUCCESS :)",
                Content = "Los cambios se han guardado correctamente.",
                CloseButtonText = "Ok"
            };
            await datosEnviados.ShowAsync();
        }
        /// <summary>
        ///     <cabecera>public async void showError()</cabecera>
        ///     <descripcion>This method shows a message caused by an exception</descripcion>
        ///     <precondiciones>None</precondiciones>
        ///     <postcondiciones>Shows an error message</postcondiciones>
        /// </summary>
        public async void showError()
        {
            ContentDialog datosEnviados = new ContentDialog
            {
                Title = "FATAL ERROR",
                Content = "Ha ocurrido un error inesperado, por favor contacte con su Fernando más cercano",
                CloseButtonText = "Ok"
            };
            await datosEnviados.ShowAsync();
        }
    }
}
