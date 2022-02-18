using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels.Utilities;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using UI.Models;

namespace UI.ViewModels
{
    public class PlayVM : clsVMBase
    {
        private bool teclaPulsadaR = false;
        private bool teclaPulsadaAR = false;
        private bool teclaPulsadaAB = false;
        private bool teclaPulsadaL = false;

        public Visibility VisibilidadUsuario { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int EnemigoX { get; set; }
        public int EnemigoY { get; set; }

        public int EnemigoX2 { get; set; }
        public int EnemigoY2 { get; set; }

        public int EnemigoX3 { get; set; }
        public int EnemigoY3 { get; set; }

        public Enemigo Enemigo1 { get; set; }
        public Enemigo Enemigo2 { get; set; }
        public Enemigo Enemigo3 { get; set; }

        public PlayVM()
        {
            X = 500;
            Y = 500;
         
            Enemigo1 = new Enemigo(1000,300);
            Enemigo1.mover();

            Enemigo2 = new Enemigo();
            Enemigo2.mover();

            Enemigo3 = new Enemigo(200,700);
            Enemigo3.mover();

        }

        public async void moverFantasma(object sender, KeyRoutedEventArgs e)
        {
            if (Enemigo.usuarioVivo)
            {
                bool pared = false;
                if (e.Key == VirtualKey.Right && !teclaPulsadaR)
                {
                    teclaPulsadaR = true;
                    teclaPulsadaAR = false;
                    teclaPulsadaAB = false;
                    teclaPulsadaL = false;
                    while (!pared && teclaPulsadaR && Enemigo.usuarioVivo)
                    {
                        if (X + 50 < 1500)
                        {
                            X += 50;
                            NotifyPropertyChanged("X");
                            Enemigo.JugadorX = X;
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
                    while (!pared && teclaPulsadaL && Enemigo.usuarioVivo)
                    {
                        if (X - 50 >= 0)
                        {
                            X -= 50;
                            NotifyPropertyChanged("X");
                            Enemigo.JugadorX = X;
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
                    while (!pared && teclaPulsadaAR && Enemigo.usuarioVivo)
                    {
                        if (Y - 50 >= 0)
                        {
                            Y -= 50;
                            NotifyPropertyChanged("Y");
                            Enemigo.JugadorY = Y;
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
                    while (!pared && teclaPulsadaAB && Enemigo.usuarioVivo)
                    {
                        if (Y + 50 < 800)
                        {
                            Y += 50;
                            NotifyPropertyChanged("Y");
                            Enemigo.JugadorY = Y;
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
}
