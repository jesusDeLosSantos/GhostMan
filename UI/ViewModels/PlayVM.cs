using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;
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
        private List<Wall> arrayPrueba = new List<Wall>(); 

        public int X { get; set; }
        public int Y { get; set; }

        public int blockX { get; set; }
        public int blockY { get; set; } 
        public PlayVM()
        {
            blockX = 500;
            blockY = 500;
            arrayPrueba.Add(new Wall(blockX, blockY));
            arrayPrueba.Add(new Wall(51, 51));
            arrayPrueba.Add(new Wall(52, 52));
            
        }
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
                        if(!canMove(X + 50, Y))
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
                        if (!canMove(X - 50, Y))
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
                        if (!canMove(X, Y - 50))
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
                    if (Y + 50 < 800 || canMove(X, Y + 50))
                    {
                        if(!canMove(X, Y + 50))
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
                    else
                    {
                        pared = true;
                    }
                }
            }
        }

        public bool canMove(int x, int y)
        {
            bool result = false;
            bool found = false;
            Wall placeholder;
            for(int i = 0; i < arrayPrueba.Count || found == true; i++)
            {
                placeholder = arrayPrueba.ElementAt(i);    
                if(placeholder.xAxis == x && placeholder.yAxis == y)
                {
                    result = true;
                } 
            }
            return result;
        }
    }
}
