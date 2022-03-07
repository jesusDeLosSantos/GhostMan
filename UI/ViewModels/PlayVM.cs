
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
using BL.manager;
using System.Data.SqlClient;

namespace UI.ViewModels
{
    public class PlayVM : clsVMBase
    {

        public int X { get; set; }
        public int Y { get; set; }

        private int velocidadX;
        private int velocidadY;
        private bool movimientoIniciado;
        private string winnerNick;
        private int puntosTotales;

        public ObservableCollection<clsElementMap> ElementMaps { get; set; }
        private List<clsElementMap> enemies = new List<clsElementMap>();
        private List<clsElementMap> elementMapsWithoutEnemiesNorEnemies;
        private List<clsElementMap> elementMapsPuntos;
        private List<clsElementType> elementTypes;

        public List<clsElementMap> Enemies { get => enemies; set => enemies = value; }
        public List<clsElementMap> ElementMapsWithoutEnemiesNorEnemies { get => elementMapsWithoutEnemiesNorEnemies; set => elementMapsWithoutEnemiesNorEnemies = value; }

        public Enemigo Enemigo1 { get; set; }
        public Enemigo Enemigo2 { get; set; }
        public Enemigo Enemigo3 { get; set; }
        public Enemigo Enemigo4 { get; set; }
        public int MapSize { get; set; }

        public PlayVM()
        {
            SharedData.FinPartida = false;
            prepararListaElementos();
            prepararDatosPuntos();
            prepararListaSoloParedes();
            initializeEnemiesList(elementTypes);
            

            //Configuracion omitir de lista con todos elementos los enemigos y el jugador
            clsElementMap player = ElementMaps.Where(x => x.IdElement == elementTypes.Where(y => y.Name.Contains("Ghost")).FirstOrDefault().Id).First(); //esto lo hariamos con un linq, pero el linq devuelve un valor que no es el correcto
            ObservableCollection<clsElementMap> elementMapsWithoutEnemiesNorPlayer = new ObservableCollection<clsElementMap>(ElementMaps.Except(enemies));
            elementMapsWithoutEnemiesNorPlayer.Remove(player);
            ElementMaps = elementMapsWithoutEnemiesNorPlayer;
            elementMapsWithoutEnemiesNorEnemies = new List<clsElementMap>(elementMapsWithoutEnemiesNorPlayer.Except(Utilidades.ListaParedes));

            try
            {
                MapSize = clsMapQueryBL.getSizeMapDAL(player.IdMap);
                NotifyPropertyChanged(nameof(MapSize));

            }
            catch (Exception) {
                Utilidades.mostrarMensajeAsync("Ocurrio un error obtener los datos");
            }
          
            prepararDatosJugabilidad(player);
            configurarEnemigos();
        }

        private void prepararDatosPuntos()
        {

            elementMapsPuntos = new List<clsElementMap>(from element in ElementMaps
                                                        where element.IdElement == 22 //No habria que poner directamente el id que corresponde con los puntos, ya que puede ser que este cambie. Lo dejamos asi por falta de tiempo
                                                        select element);
            puntosTotales = elementMapsPuntos.Count;
        }

        private void prepararListaElementos()
        {
            try
            {
                elementTypes = clsElementTypeQueryBL.getListOfElementTypeBL();
                ElementMaps = new ObservableCollection<clsElementMap>(SharedData.MapSelectedToPlay);
                if (SharedData.IsCommunityMap)
                {
                    foreach (var elementMap in ElementMaps)
                    {
                        elementMap.AxisX *= 50;
                        elementMap.AxisY *= 50;
                    }
                }
            }
            catch (Exception)
            {
                Utilidades.mostrarMensajeAsync("Ocurrio un error obtener los datos necesarios para el juego");
            }
        }

        private void configurarEnemigos()
        {
            for (int i = enemies.Count; i > 0; i--)
            {
                switch (i)
                {
                    case 4:
                        Enemigo4 = new Enemigo(enemies[3].AxisX, enemies[3].AxisY,16);//No habria que poner directamente el id que corresponde con los puntos, ya que puede ser que este cambie. Lo dejamos asi por falta de tiempo
                        Enemigo4.mover();
                        break;
                    case 3:
                        Enemigo3 = new Enemigo(enemies[2].AxisX, enemies[2].AxisY,17);
                        Enemigo3.mover();
                        break;
                    case 2:
                        Enemigo2 = new Enemigo(enemies[1].AxisX, enemies[1].AxisY,18);
                        Enemigo2.mover();
                        break;
                    case 1:
                        Enemigo1 = new Enemigo(enemies[0].AxisX, enemies[0].AxisY,19);
                        Enemigo1.mover();
                        break;
                }
            }
        }
        private void prepararListaSoloParedes()
        {
            Utilidades.ListaParedes = new List<clsElementMap>(from element in ElementMaps
                                                              where element.IdElement == (from elementType in elementTypes
                                                                                          where (element.IdElement == elementType.Id) && elementType.Name.Contains("wall")
                                                                                          select elementType.Id).FirstOrDefault()
                                                              select element);
        }

        private void initializeEnemiesList(List<clsElementType> elementTypes)
        {
            /*(from elements in elementMaps
            where elements.IdElement == (from elementType in elementTypes
                where (elements.IdElement == elementType.Id) && elementType.Name.Contains("Exorcist")
                    select elementType.Id).FirstOrDefault()
            select elements); ESTE LINQ NO FUNCIONA, NO SABEMOS POR QUE, TRAE MAS ELEMENTOS DE LOS QUE DEBERIA */
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

        private void prepararDatosJugabilidad(clsElementMap player)
        {
            X = player.AxisX;
            Y = player.AxisY;
            velocidadX = 0;
            velocidadY = 0;
            movimientoIniciado = false;
            Enemigo.PlayerPositionX = X;
            Enemigo.PlayerPositionY = Y;
        }

        public async Task moverFantasma()
        {
            while (!SharedData.FinPartida)
            {
                if (velocidadX != 0 && Utilidades.canMove(X + velocidadX, Y)
                    && (X > 0 && velocidadX < 0 || X < SharedData.MaxMapWidth - 50 && velocidadX > 0))
                {
                    X += velocidadX;
                    Enemigo.PlayerPositionX = X;
                    comprobarRecogerPuntos();//Esta duplicado en ambos lados porque solo se tiene que comprobar cuando se modifique la posicion del jugador(Cuando se mueva)
                }
                else
                {
                    velocidadX = 0;
                }
                if (velocidadY != 0 && Utilidades.canMove(X, Y + velocidadY)
                    && (Y > 0 && velocidadY < 0 || Y < 750 && velocidadY > 0))
                {
                    Y += velocidadY;
                    Enemigo.PlayerPositionY = Y;
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
                    winnerNick = await Utilidades.gameWon();
                    try
                    {
                        clsLeaderboardManagerBL.postLeaderboardBL(new clsLeaderboard(SharedData.MapSelectedToPlay.First().IdMap, winnerNick, SharedData.CompletionTime));
                    }
                    catch (Exception)
                    {
                        //Mensaje de: No se pudo guardar la puntuacion
                    }

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
                        if (velocidadX == -50)//Para que no haga una pausa cuando se cambie en la direccion opuesta
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
                        if (velocidadY == 50)//Para que no haga una pausa cuando se cambie en la direccion opuest
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
                    elementMapsPuntos.Remove(elementMapsPuntos[i]);//Se elimina de esta lista tambien, para que la proxima vez sea un elementos menos que iterar
                    puntoRecogido = true;
                    puntosTotales--;
                }
            }
        }
    }
}

