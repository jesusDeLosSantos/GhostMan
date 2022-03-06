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
using Entities;
using BL.query;
using System.Collections.ObjectModel;

namespace UI.ViewModels
{
    public class PlayVM : clsVMBase
    {
        public Visibility VisibilidadUsuario { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        private int velocidadX, velocidadY;
        bool movimientoIniciado;

        public ObservableCollection<clsElementMap> ElementMaps { get; set; }
        private List<clsElementMap> enemies = new List<clsElementMap>();
        private List<clsElementMap> elementMapsWithoutEnemiesNorEnemies;
        private List<clsElementMap> elementMapsPuntos;
        private int puntosTotales;

        public List<clsElementMap> Enemies { get => enemies; set => enemies = value; }
        public List<clsElementMap> ElementMapsWithoutEnemiesNorEnemies { get => elementMapsWithoutEnemiesNorEnemies; set => elementMapsWithoutEnemiesNorEnemies = value; }


        public Enemigo Enemigo1 { get; set; }
        public Enemigo Enemigo2 { get; set; }
        public Enemigo Enemigo3 { get; set; }
        public Enemigo Enemigo4 { get; set; }

        public PlayVM()
        {
            List<clsElementType> elementTypes = clsElementTypeQueryBL.getListOfElementTypeBL();
            ElementMaps = new ObservableCollection<clsElementMap>(SharedData.MapSelectedToPlay);
            if (SharedData.IsCommunityMap)
            {
                foreach (var elementMap in ElementMaps)
                {
                    elementMap.AxisX *= 50;
                    elementMap.AxisY *= 50;
                }
            }

            elementMapsPuntos = new List<clsElementMap>(from element in ElementMaps
                                                        where element.IdElement == 22
                                                        select element);
            puntosTotales = elementMapsPuntos.Count;

            Utilidades.listaParedes = new List<clsElementMap>(from element in ElementMaps
                                                              where element.IdElement == (from elementType in elementTypes
                                                                                          where (element.IdElement == elementType.Id) && elementType.Name.Contains("wall")
                                                                                          select elementType.Id).FirstOrDefault()
                                                              select element);

            initializeEnemiesList(elementTypes);
            clsElementMap player = ElementMaps.Where(x => x.IdElement == elementTypes.Where(y => y.Name.Contains("Ghost")).FirstOrDefault().Id).First(); //esto lo hariamos con un linq, pero el linq devuelve un valor que no es el correcto
            ObservableCollection<clsElementMap> elementMapsWithoutEnemiesNorPlayer = new ObservableCollection<clsElementMap>(ElementMaps.Except(enemies));
            elementMapsWithoutEnemiesNorPlayer.Remove(player);
            ElementMaps = elementMapsWithoutEnemiesNorPlayer;
            elementMapsWithoutEnemiesNorEnemies = new List<clsElementMap>(elementMapsWithoutEnemiesNorPlayer.Except(Utilidades.listaParedes));

            /*(from elements in elementMaps
            where elements.IdElement == (from elementType in elementTypes
                where (elements.IdElement == elementType.Id) && elementType.Name.Contains("Exorcist")
                    select elementType.Id).FirstOrDefault()
            select elements); ESTE LINQ NO FUNCIONA, NO SABEMOS POR QUE, TRAE MAS ELEMENTOS DE LOS QUE DEBERIA */
            X = player.AxisX;
            Y = player.AxisY;
            velocidadX = 0;
            velocidadY = 0;
            movimientoIniciado = false;
            Enemigo.JugadorX = X;
            Enemigo.JugadorY = Y;

            for (int i = enemies.Count; i > 0; i--)
            {
                switch (i)
                {
                    case 4:
                        Enemigo4 = new Enemigo(enemies[3].AxisX, enemies[3].AxisY, 16);
                        Enemigo4.moverFantasma();
                        break;
                    case 3:
                        Enemigo3 = new Enemigo(enemies[2].AxisX, enemies[2].AxisY, 17);
                        Enemigo3.moverFantasma();
                        break;
                    case 2:
                        Enemigo2 = new Enemigo(enemies[1].AxisX, enemies[1].AxisY, 18);
                        Enemigo2.moverFantasma();
                        break;
                    case 1:
                        Enemigo1 = new Enemigo(enemies[0].AxisX, enemies[0].AxisY, 19);
                        Enemigo1.moverFantasma();
                        break;
                }
            }

        }

        private void initializeEnemiesList(List<clsElementType> elementTypes)
        {
            foreach (var element in ElementMaps)
            {
                foreach (var elementType in elementTypes)
                {
                    if (element.IdElement == elementType.Id && elementType.Name.Contains("Exorcist"))
                    {
                        enemies.Add(element);
                    }
                }
            }
        }

        public async Task moverFantasma()
        {
            while (!SharedData.FinPartida)
            {
                if (velocidadX != 0 && Utilidades.canMove(X + velocidadX, Y)
                    && (X > 0 && velocidadX < 0 || X < 1450 && velocidadX > 0))
                {
                    X += velocidadX;
                    Enemigo.JugadorX = X;
                    comprobarRecogerPuntos();
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
                    comprobarRecogerPuntos();
                }
                else
                {
                    velocidadY = 0;
                }
                NotifyPropertyChanged("X");
                NotifyPropertyChanged("Y");
                await Task.Delay(200);
                if (puntosTotales == 0)
                {
                    SharedData.FinPartida = true;
                }
            }

        }

        public async void determinarMovilidadFantasma(object sender, KeyRoutedEventArgs e)
        {
            if (!SharedData.FinPartida)
            {
                switch (e.Key)
                {
                    case VirtualKey.Right:
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
                    case VirtualKey.Left:
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
                    case VirtualKey.Up:
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
                    case VirtualKey.Down:
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

                if (!movimientoIniciado)
                {
                    movimientoIniciado = true;
                    await moverFantasma();
                }
            }
        }
        private void comprobarRecogerPuntos()
        {
            bool puntoRecogido = false;
            for (int i = 0; i < elementMapsPuntos.Count && !puntoRecogido; i++)
            {
                if (X == elementMapsPuntos[i].AxisX && Y == elementMapsPuntos[i].AxisY)
                {
                    ElementMaps.Remove(elementMapsPuntos[i]);
                    elementMapsPuntos.Remove(elementMapsPuntos[i]);
                    puntoRecogido = true;
                    puntosTotales--;
                }
            }
        }
    }
}

