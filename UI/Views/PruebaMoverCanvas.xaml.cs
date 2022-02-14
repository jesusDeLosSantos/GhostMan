using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UI.ViewModels;
using System.ComponentModel;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UI.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PruebaMoverCanvas : Page
    {
        bool teclaPulsadaR = false;
        bool teclaPulsadaL = false;
        public PruebaMoverCanvas()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        async void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            bool pared = false;
            CustomModeVM a = (CustomModeVM)this.DataContext;
            if (e.VirtualKey == VirtualKey.Right && !teclaPulsadaR) {
                teclaPulsadaR = true;
                teclaPulsadaL = false;
                while (!pared && !teclaPulsadaL)
                {
                    
                    if (a.X + 50 != 800)
                    {
                        a.X += 50;
                        if () {
                            break; }
                    }
                    else { 
                        pared = true;
                    }
                }
            }
            if (e.VirtualKey == VirtualKey.Left && !teclaPulsadaL)
            {
                teclaPulsadaR = false;
                teclaPulsadaL = true;
                while (!pared && !teclaPulsadaR)
                {
                    if (a.X - 50 != 0)
                    {
                        a.X -= 50;
                        
                       
                    }
                    else
                    {
                        pared = true;
                        teclaPulsadaL = false;
              
                    }
                }
            }
            await System.Threading.Tasks.Task.Delay(200);
        }


    }
}
