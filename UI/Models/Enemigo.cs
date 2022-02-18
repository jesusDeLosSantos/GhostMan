using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels.Utilities;

namespace UI.Models
{
    public class Enemigo : clsVMBase
    {

        public Enemigo() {
            X = 0;
            Y = 0;
        }

        public Enemigo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static int JugadorX { get; set; }
        public static int JugadorY { get; set; }

        public static bool usuarioVivo = true;

        public async void mover()
        {
            Random random = new Random();
            int aletorio;
            while (usuarioVivo)
            {
                aletorio = random.Next(4);
                switch (aletorio)
                {
                    case 0: //IZQ
                        while (X - 50 >= 0 && usuarioVivo)
                        {
                            X -= 50;  NotifyPropertyChanged("X");
                          
                            comprobarEliminarUsuario();
                            await Task.Delay(200);
                        }
                        break;
                    case 1: //DER
                        while (X + 50 < 1500 && usuarioVivo)
                        {
                            X += 50;NotifyPropertyChanged("X");
                            
                            comprobarEliminarUsuario();
                            await Task.Delay(200);
                        }
                        break;
                    case 2: //ARR
                        while (Y - 50 >= 0 && usuarioVivo)
                        {
                            Y -= 50;
                            comprobarEliminarUsuario();
                            NotifyPropertyChanged("Y");
                            await Task.Delay(200);
                        }
                        break;

                    case 3: //ABA
                        while (Y + 50 < 800 && usuarioVivo)
                        {
                            Y += 50; 
                            comprobarEliminarUsuario();
                            NotifyPropertyChanged("Y");
                            await Task.Delay(200);
                        }
                        break;
                }
            }
        }
        private void comprobarEliminarUsuario()
        {
            if (X == JugadorX && Y == JugadorY)
            {
                usuarioVivo = false;
            }
        }
    }
}
