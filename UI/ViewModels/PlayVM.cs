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
        public Visibility VisibilidadUsuario { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        private int velocidadX, velocidadY;
        bool movimientoIniciado;


        public Enemigo Enemigo1 { get; set; }
        public Enemigo Enemigo2 { get; set; }
        public Enemigo Enemigo3 { get; set; }

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
            List<Wall> arrayPrueba = new List<Wall>();

            arrayPrueba.Add(wall = new Wall(0, 0));
            arrayPrueba.Add(wall2 = new Wall(100, 450));
            arrayPrueba.Add(wall3 = new Wall(450, 500));
            arrayPrueba.Add(wall4 = new Wall(350, 600));
            arrayPrueba.Add(wall5 = new Wall(750, 200));
            arrayPrueba.Add(wall6 = new Wall(800, 0));
            arrayPrueba.Add(wall7 = new Wall(1000, 500));
            arrayPrueba.Add(wall8 = new Wall(1450, 750));

            Utilidades.listaParedes = arrayPrueba;

            X = 150;
            Y = 450;
            velocidadX = 0;
            velocidadY = 0;
            movimientoIniciado = false;
            Enemigo.JugadorX = X;
            Enemigo.JugadorY = Y;
            Enemigo1 = new Enemigo(100, 150, arrayPrueba);
            Enemigo1.moverFantasma();

            Enemigo2 = new Enemigo(0, 50, arrayPrueba);
            Enemigo2.moverFantasma();

            Enemigo3 = new Enemigo(200, 700, arrayPrueba);
            Enemigo3.moverFantasma();
        }

        public async Task moverFantasma()
        {
            while (Enemigo.usuarioVivo)
            {
                if (velocidadX != 0 && Utilidades.canMove(X + velocidadX, Y)
                    && (X > 0 && velocidadX < 0 || X < 1450 && velocidadX > 0))
                {
                    X += velocidadX;
                    Enemigo.JugadorX = X;
                }
                else
                {
                    velocidadX = 0;
                }
                if (velocidadY != 0 && Utilidades.canMove(X, Y + velocidadY)
                    && (Y > 0 && velocidadY < 0 || Y < 750 && velocidadY > 0))
                {
                    Y += velocidadY;
                    Enemigo.JugadorY = Y;
                }
                else
                {
                    velocidadY = 0;
                }
                NotifyPropertyChanged("X");
                NotifyPropertyChanged("Y");
                await Task.Delay(200);
            }
            VisibilidadUsuario = Visibility.Collapsed;
            NotifyPropertyChanged("VisibilidadUsuario");
        }

        public async void determinarMovilidadFantasma(object sender, KeyRoutedEventArgs e)
        {
            if (Enemigo.usuarioVivo)
            {
                switch (e.Key)
                {
                    case VirtualKey.Right:
                        if (velocidadX == -50)//Para que no haga la pausa cuando se cambie en la direccion opuesta
                        {
                            velocidadX += 100;
                        }
                        else if(velocidadX < 50)
                        {
                            velocidadX += 50;
                            velocidadY = 0;
                        }
                        break;
                    case VirtualKey.Left:
                        if (velocidadX == 50) {
                            velocidadX -= 100;
                        }else if (velocidadX > -50)
                        {
                            velocidadX -= 50;
                            velocidadY = 0;
                        }

                        break;
                    case VirtualKey.Up:
                        if (velocidadY == 50)
                        {
                            velocidadY -= 100;
                        }
                        else if(velocidadY > -50)
                        {
                            velocidadY -= 50;
                            velocidadX = 0;
                        }
                        break;
                    case VirtualKey.Down:
                        if (velocidadY == -50)
                        {
                            velocidadY += 100;
                        }
                        else if(velocidadY < 50)
                        {
                            velocidadY += 50;
                            velocidadX = 0;
                        }
                        break;
                }

                if (!movimientoIniciado)
                {
                    movimientoIniciado = true;
                    await moverFantasma();
                }
            }
        }
    }
}

