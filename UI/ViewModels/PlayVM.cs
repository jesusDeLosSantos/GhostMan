using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels.Utilities;
using Windows.System;
using Windows.UI.Xaml.Input;

namespace UI.ViewModels
{
    public class PlayVM : clsVMBase
    {
        private bool teclaPulsadaR = false;
        private bool teclaPulsadaAR = false;
        private bool teclaPulsadaAB = false;
        private bool teclaPulsadaL = false;

        public int X { get; set; }
        public int Y { get; set; }

        public async void moverFantasma(object sender, KeyRoutedEventArgs e)
        {
            bool pared = false;
            if (e.Key == VirtualKey.Right && !teclaPulsadaR)
            {
                teclaPulsadaR = true;
                teclaPulsadaAR = false;
                teclaPulsadaAB = false;
                teclaPulsadaL = false;
                while (!pared && teclaPulsadaR)
                {
                    if (X + 50 < 1500)
                    {
                        X += 50;
                        NotifyPropertyChanged("X");
                        await Task.Delay(200); //System.Threading.Tasks.Task.Delay(200);
                    }
                    else
                    {
                        pared = true;
                    }
                }
            }
            else if (e.Key == VirtualKey.Left && !teclaPulsadaL)
            {
                teclaPulsadaR = false;
                teclaPulsadaAR = false;
                teclaPulsadaAB = false;
                teclaPulsadaL = true;
                while (!pared && teclaPulsadaL)
                {
                    if (X - 50 >= 0)
                    {
                        X -= 50;
                        NotifyPropertyChanged("X");
                        await Task.Delay(200);
                    }
                    else
                    {
                        pared = true;
                    }
                }
            }
            else if (e.Key == VirtualKey.Up && !teclaPulsadaAR)
            {
                teclaPulsadaR = false;
                teclaPulsadaAR = true;
                teclaPulsadaAB = false;
                teclaPulsadaL = false;
                while (!pared && teclaPulsadaAR)
                {
                    if (Y - 50 >= 0)
                    {
                        Y -= 50;
                        NotifyPropertyChanged("Y");
                        await Task.Delay(200);
                    }
                    else
                    {
                        pared = true;
                    }
                }
            }
            else if (e.Key == VirtualKey.Down && !teclaPulsadaAB)
            {
                teclaPulsadaR = false;
                teclaPulsadaAR = false;
                teclaPulsadaAB = true;
                teclaPulsadaL = false;
                while (!pared && teclaPulsadaAB)
                {
                    if (Y + 50 < 800)
                    {
                        Y += 50;
                        NotifyPropertyChanged("Y");
                        await Task.Delay(200);
                    }
                    else
                    {
                        pared = true;
                    }
                }
            }
        }
    }
}
