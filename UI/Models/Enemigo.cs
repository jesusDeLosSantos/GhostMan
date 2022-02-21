using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels.Utilities;

namespace UI.Models
{
    public class Enemigo : clsVMBase
    {

        public Enemigo()
        {
            X = 0;
            Y = 0;
        }

        public Enemigo(int x, int y, List<Wall> arrayPrueba)
        {
            X = x;
            Y = y;
            this.arrayPrueba = arrayPrueba;
        }
        public List<Wall> arrayPrueba;
        public int X { get; set; }
        public int Y { get; set; }

        //JugadorX y JugadorY, se iran setteando continuamente, cada vez que la posicion del usuario cambie
        public static int JugadorX { get; set; }
        public static int JugadorY { get; set; }

        public static bool usuarioVivo = true;
        public bool derPared = false;
        public bool izqPared = false;
        public bool arrPared = false;
        public bool abaPared = false;


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
                        while (X - 50 >= 0 && usuarioVivo && !izqPared)
                        {
                            if (!canMove(X - 50, Y))
                            {
                                derPared = false;
                                arrPared = false;
                                abaPared = false;
                                X -= 50; 
                                NotifyPropertyChanged("X");
                                comprobarEliminarUsuario();
                                await Task.Delay(200);
                            }
                            else {
                               izqPared = true;    
                            }
                            
                        }
                        break;
                    case 1: //DER
                        while (X + 50 < 1500 && usuarioVivo && !derPared)
                        {

                            if (!canMove(X + 50, Y))
                            {
                                izqPared = false;
                                arrPared = false;
                                abaPared = false;

                                X += 50; 
                                NotifyPropertyChanged("X");
                                comprobarEliminarUsuario();
                                await Task.Delay(200);
                            }
                            else
                            {
                                derPared = true;    
                            }
                        }
                        break;
                    case 2: //ARR
                        while (Y - 50 >= 0 && usuarioVivo && !arrPared)
                        {
                            if (!canMove(X, Y - 50))
                            {
                                derPared = false;
                                izqPared = false;
                                abaPared = false;
                                Y -= 50;
                                
                                NotifyPropertyChanged("Y");
                                comprobarEliminarUsuario();
                                await Task.Delay(200);
                            }
                            else
                            {
                                arrPared = true;    
                            }

                        }
                        break;

                    case 3: //ABA
                        while (Y + 50 < 800 && usuarioVivo && !abaPared)
                        {
                            if (!canMove(X,Y + 50))
                            {
                                derPared = false;
                                arrPared = false;
                                izqPared = false;
                                Y += 50;
                                
                                NotifyPropertyChanged("Y");
                                comprobarEliminarUsuario();
                                await Task.Delay(200);
                            }
                            else
                            {
                                abaPared = true;
                            }

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

        public bool canMove(int x, int y)
        {
            bool result = false;
            Wall placeholder;
            for (int i = 0; i < arrayPrueba.Count && !result; i++)
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
