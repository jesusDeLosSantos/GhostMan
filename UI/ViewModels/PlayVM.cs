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
        private List<Wall> arrayPrueba = new List<Wall>();
        public Visibility VisibilidadUsuario { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Enemigo Enemigo1 { get; set; }
        /*public Enemigo Enemigo2 { get; set; }
        public Enemigo Enemigo3 { get; set; }*/

        public Wall wall { get; set; }
        public Wall wall2 { get; set; }
        public Wall wall3 { get; set; }
        public Wall wall4 { get; set; }
        public Wall wall5 { get; set; }
        public Wall wall6 { get; set; }
        public Wall wall7 { get; set; }
        public Wall wall8 { get; set; }
        public PlayVM()
        {

            arrayPrueba.Add(wall = new Wall(0, 0));
            arrayPrueba.Add(wall2 = new Wall(100, 450));
            arrayPrueba.Add(wall3 = new Wall(450, 500));
            arrayPrueba.Add(wall4 = new Wall(350, 600));
            arrayPrueba.Add(wall5 = new Wall(750, 200));
            arrayPrueba.Add(wall6 = new Wall(800, 0));
            arrayPrueba.Add(wall7 = new Wall(1000, 500));
            arrayPrueba.Add(wall8 = new Wall(1450, 750));



            X = 150;
            Y = 450;
            Enemigo.JugadorX = X;
            Enemigo.JugadorY = Y;
            Enemigo1 = new Enemigo(100,150,arrayPrueba);
            Enemigo1.mover();
         
           /* Enemigo2 = new Enemigo(0,50,arrayPrueba);
            Enemigo2.mover();

            Enemigo3 = new Enemigo(200,700,arrayPrueba);
            Enemigo3.mover();*/
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
                            if (!canMove(X + 50, Y))
                            {
                               
                                X += 50;
                                NotifyPropertyChanged("X");Enemigo.JugadorX = X;
                                await Task.Delay(200); //System.Threading.Tasks.Task.Delay(200);
                                                       
                            }
                            else
                            {
                                pared = true;
                            }
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
                            if (!canMove(X - 50, Y))
                            {
                                  
                                X -= 50;
                                Enemigo.JugadorX = X;
                                NotifyPropertyChanged("X");
                              
                                await Task.Delay(200);
                            }
                            else
                            {
                                pared = true;
                            }
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
                            if (!canMove(X, Y - 50))
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
                            if (!canMove(X, Y + 50))
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
                        else
                        {
                            pared = true;
                        }
                    }
                }
            }
        }
        public bool canMove(int x, int y)
        {
            bool result = false;
            bool found = false;
            Wall placeholder;
            for (int i = 0; i < arrayPrueba.Count || found == true; i++)
            {
                placeholder = arrayPrueba.ElementAt(i);
                if (placeholder.xAxis == x && placeholder.yAxis == y)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
