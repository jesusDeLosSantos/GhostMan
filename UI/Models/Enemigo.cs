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

        public Enemigo(int x, int y, int idElement)
        {
            X = x;
            Y = y;
            IdElement = idElement;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int IdElement { get; set; }

        //JugadorX y JugadorY, se iran setteando continuamente, cada vez que la posicion del usuario cambie
        public static int JugadorX { get; set; }
        public static int JugadorY { get; set; }

        public static bool usuarioVivo = true;

        private int velocidadX;
        private int velocidadY;

        public async void moverFantasma()
        {
            Random random = new Random();
            int cambioDireccionAleatorio;//De esta manera cambiar de direccion de manera mas realista, porque sino solo cambiaria de direccion cuando no se pueda mover


            while (usuarioVivo)
            {
                cambioDireccionAleatorio = random.Next(6);
                if (velocidadX != 0 && Utilidades.canMove(X + velocidadX, Y)
                    && (X > 0 && velocidadX < 0 || X < 1450 && velocidadX > 0))
                {
                    X += velocidadX;
                    if (cambioDireccionAleatorio == 1)
                    {
                        determinarMovilidadFantasma();
                    }
                }
                else
                {
                    velocidadX = 0;
                    if (velocidadY == 0)
                    {
                        determinarMovilidadFantasma();
                    }

                }

                if (velocidadY != 0 && Utilidades.canMove(X, Y + velocidadY)
                    && (Y > 0 && velocidadY < 0 || Y < 750 && velocidadY > 0))
                {
                    Y += velocidadY;
                    if (cambioDireccionAleatorio == 1)
                    {
                        determinarMovilidadFantasma();
                    }
                }
                else
                {
                    velocidadY = 0;
                    if (velocidadX == 0)
                    {
                        determinarMovilidadFantasma();
                    }
                }

                NotifyPropertyChanged("X");
                NotifyPropertyChanged("Y");
                comprobarEliminarUsuario();
                await Task.Delay(200);
            }
        }


        public void determinarMovilidadFantasma()
        {
            Random random = new Random();
            int aletorio;

            aletorio = random.Next(4);
            switch (aletorio)
            {
                case 0:
                    if (velocidadX == -50)//Para que no haga la pausa cuando se cambie en la direccion opuesta
                    {
                        velocidadX += 100;
                    }
                    else if (velocidadX < 50)
                    {
                        velocidadX += 50;
                        velocidadY = 0;
                    }
                    break;
                case 1:
                    if (velocidadX == 50)
                    {
                        velocidadX -= 100;
                    }
                    else if (velocidadX > -50)
                    {
                        velocidadX -= 50;
                        velocidadY = 0;
                    }

                    break;
                case 2:
                    if (velocidadY == 50)
                    {
                        velocidadY -= 100;
                    }
                    else if (velocidadY > -50)
                    {
                        velocidadY -= 50;
                        velocidadX = 0;
                    }
                    break;
                case 3:
                    if (velocidadY == -50)
                    {
                        velocidadY += 100;
                    }
                    else if (velocidadY < 50)
                    {
                        velocidadY += 50;
                        velocidadX = 0;
                    }
                    break;
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
