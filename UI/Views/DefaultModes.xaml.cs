using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UI.Views
{
    public sealed partial class VistaJugar : Page
    {
        public VistaJugar()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// This method decides where to navigate depending on the name of the pressed button
        /// </summary>
        /// <param name="sender">Pressed button</param>
        /// <param name="e"></param>
        private void NavigateToPlay(object sender, RoutedEventArgs e)
        {
            var buttonPressed = sender as Button;
            switch (buttonPressed.Name)
            {
                case "btnEasy":
                    Frame.Navigate(typeof(Prueba));
                    break;
                case "btnMedium":
                    Frame.Navigate(typeof(PruebaMedio));
                    break;
                case "btnHard":
                    Frame.Navigate(typeof(PruebaDificil));
                    break;
            }
        }
    }
}
