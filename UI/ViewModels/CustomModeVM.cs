using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using UI.Models;
using UI.ViewModels.Utilities;
using BL.query;

namespace UI.ViewModels
{
    public class CustomModeVM : clsVMBase
    {
        //TOOD HACER QUE LA LISTA PUNTUACION DE LOS MAPAS VENGAN YA ORDENADA DE LA BBDD 
        //CUANDO SE VAYA A CARGAR LOS SIGUIENTES MAPAS DE LA BASE DE DATOS, HACERLO ASYNC PARA QUE EL USUARIO SIGA INTERACTUANDO CON LA LISTA MIENTRAS SE CARGAN LOS SIGUIENTES MAPAS
        #region Attributes
        ObservableCollection<clsMapLeaderboard> originalMapList;
        ObservableCollection<clsMapLeaderboard> nextOriginalMapList;
        ObservableCollection<clsMapLeaderboard> mapList;
        clsMapLeaderboard mapSelected;
        string inputText;
        DelegateCommand leftFilterButtonCommand;
        DelegateCommand rightFilterButtonCommand;
        int posicionSiguienteMapa = 0;
        int posicionAnteriorMapa = 0;
        #endregion

        #region Builders
        public CustomModeVM()
        {
            rightFilterButtonCommand = new DelegateCommand(RightFilterButtonCommand_Executed, RightFilterButtonCommand_CanExecuted);
            leftFilterButtonCommand = new DelegateCommand(LeftFilterButtonCommand_Executed, LeftFilterButtonCommand_CanExecuted);
            crearMapasDePrueba();
            cargarMapasPosicionEspecificada(posicionSiguienteMapa);
            posicionSiguienteMapa += 10;
            //cargarSiguientesMapas(); //Se cargan los 10 primeros mapas
        }
        #endregion

        #region Getters & Setters
        public ObservableCollection<clsMapLeaderboard> MapList { get => mapList; }
        public clsMapLeaderboard MapSelected
        {
            get
            {
                return mapSelected;
            }
            set
            {
                mapSelected = value;
                NotifyPropertyChanged("MapSelected");
            }
        }
        public string InputText
        {
            get
            {
                return inputText;
            }
            set
            {
                inputText = value;
                NotifyPropertyChanged("InputText");

                if (string.IsNullOrEmpty(value))
                { //Si se borra todo el contenido del input text
                    mapList = originalMapList;
                    NotifyPropertyChanged("MapList");
                }
                else
                {
                    filterList();
                }
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                leftFilterButtonCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion
        #region Commands
        public DelegateCommand LeftFilterButtonCommand
        {
            get { return leftFilterButtonCommand; }
        }

        private void LeftFilterButtonCommand_Executed()
        {

            //posicionAnteriorMapa = posicionSiguienteMapa-20;
            if (posicionAnteriorMapa == 10) //En la page 2, se obtienen los 50 mapas anteriores
            {
                crearMapasDePrueba2();
            }

            if (posicionAnteriorMapa == 0)//Cuando la posicion siguiente mapa sea 50 significa que se paso de la pagina 5 y se quiere ir a la siguiente
            {
                crearMapasDePrueba2();
                //if(List que me devuelve la BBDD.Count > 0)
                originalMapList = nextOriginalMapList; //Se llama a la bbdd entonces la condicion del comand es si la bbdd la lista de mapas que me da tiene elementos
                originalMapList.OrderByDescending(mapa => mapa.Nick);
                //dentro del if cargarSiguientesMapas();
                cargarMapasPosicionEspecificada(posicionAnteriorMapa=40);//Es te eliminarlo cuando se ponga el if comentado
                posicionSiguienteMapa = posicionAnteriorMapa;
            }
            else
            {
                posicionAnteriorMapa = posicionSiguienteMapa -10;
                cargarMapasPosicionEspecificada(posicionAnteriorMapa);
                posicionSiguienteMapa = posicionAnteriorMapa;
            }

        }

        private bool LeftFilterButtonCommand_CanExecuted()
        {
            //TODO Controlar cuando se llegue al limite por la izquierda
            return mapList.Count >= 10;
        }

        public DelegateCommand RightFilterButtonCommand
        {
            get { return rightFilterButtonCommand; }
        }

        private async void RightFilterButtonCommand_Executed()
        {
            if (posicionSiguienteMapa == 30) //En la page 4, se obtienen los 50 siguientes mapas
            {
                List<clsMap> a = await Task.Run(() => { return clsMapQueryBL.getListOfCustomMapsBL(); } );
            }

            if (posicionSiguienteMapa == 40)//Cuando la posicion siguiente mapa sea 50 significa que se paso de la pagina 5 y se quiere ir a la siguiente
            {
                //if(List que me devuelve la BBDD.Count > 0)
                originalMapList = nextOriginalMapList; //Se llama a la bbdd entonces la condicion del comand es si la bbdd la lista de mapas que me da tiene elementos
                //dentro del if cargarSiguientesMapas();
                cargarMapasPosicionEspecificada(posicionSiguienteMapa=0);//Es te eliminarlo cuando se ponga el if comentado;
                posicionAnteriorMapa = posicionSiguienteMapa;
            }
            else {
                posicionSiguienteMapa = posicionAnteriorMapa + 10;
                cargarMapasPosicionEspecificada(posicionSiguienteMapa);
                posicionAnteriorMapa = posicionSiguienteMapa;

            }
        }

        private bool RightFilterButtonCommand_CanExecuted()
        {
            //TODO Controlar cuando se llegue al limite por la Derecha
            //return posicionSiguineteMapa < 50;
            //TODO CUANDO SE LLEGUE A 50 HAY QUE COMPROBAR SI EN LA BBDD HAY MAS MAPAS
            return mapList.Count >= 10 || (nextOriginalMapList.Count > 0 && posicionSiguienteMapa == 50);//posicionSiguienteMapa <= originalMapList.Count - 1;
        }
        #endregion

        #region Methods 
        private void cargarMapasPosicionEspecificada(int posicion)
        {
            int numeroDeSiguientesMapas = 10;
            if ((posicion + 10) > originalMapList.Count - 1)
            {
                numeroDeSiguientesMapas = originalMapList.Count - posicion;
            }
            mapList = new ObservableCollection<clsMapLeaderboard>
                    (originalMapList.ToList().GetRange(posicion, numeroDeSiguientesMapas));
            NotifyPropertyChanged("MapList");

            if (mapList.Count != 0)
            {
                mapSelected = mapList[0];
                NotifyPropertyChanged("MapSelected");
            }
            rightFilterButtonCommand.RaiseCanExecuteChanged();
        }


        /*
        private void cargarSiguientesMapas()
        {
            int numeroDeSiguientesMapas = 10;
            if ((posicionSiguienteMapa + 10) > originalMapList.Count - 1)
            {
                numeroDeSiguientesMapas = originalMapList.Count - posicionSiguienteMapa;
            }
            posicionSiguienteMapa += 10;
            mapList = new ObservableCollection<clsMapLeaderboard>
                    (originalMapList.ToList().GetRange(posicionSiguienteMapa, numeroDeSiguientesMapas));
            NotifyPropertyChanged("MapList");

            if (mapList.Count != 0)
            {
                mapSelected = mapList[0];
                NotifyPropertyChanged("MapSelected");
            }
            rightFilterButtonCommand.RaiseCanExecuteChanged();
        }

        private void cargarMapasAnteriores()
        {
            int numeroDeSiguientesMapas = 10;

            posicionSiguienteMapa -= 10;
            mapList = new ObservableCollection<clsMapLeaderboard>
                    (originalMapList.ToList().GetRange(posicionSiguienteMapa-10, numeroDeSiguientesMapas));
            NotifyPropertyChanged("MapList");

            if (mapList.Count != 0)
            {
                mapSelected = mapList[0];
                NotifyPropertyChanged("MapSelected");
            }
        }*/

        private void filterList()
        {
            mapList = new ObservableCollection<clsMapLeaderboard>(from map in originalMapList
                                                                  where map.Nick.ToLower().Contains(inputText.ToLower())
                                                                  select map);
            NotifyPropertyChanged("MapList");
        }

        public List<clsLeaderboard> getLeaderboards() {
            List<clsLeaderboard> list = new List<clsLeaderboard>();
            list.Add(new clsLeaderboard(1,"Jugador 1", 1111111111));
            list.Add(new clsLeaderboard(1,"Jugador 2", 2));
            list.Add(new clsLeaderboard(1,"Jugador 3", 0));
            list.Add(new clsLeaderboard(1,"Jugador 4", 1));            
            list.Add(new clsLeaderboard(1,"Jugador 5", 10));
            list.Add(new clsLeaderboard(1,"Jugador 6", 1000));
            list.Add(new clsLeaderboard(1, "Jugador 1", 1111111111));
            list.Add(new clsLeaderboard(1, "Jugador 2", 2));
            list.Add(new clsLeaderboard(1, "Jugador 3", 0));
            list.Add(new clsLeaderboard(1, "Jugador 4", 1));
            return list;
        }
        #endregion

        private void crearMapasDePrueba()
        {
            //No hace falta ordenar ya vendria de la base datos 
            List<clsLeaderboard> leaderboards = new List<clsLeaderboard>(getLeaderboards().OrderByDescending(leaderboard => leaderboard.Score)); //Order Leaderboards of mapaseled by Score
            List<clsLeaderboardWithPosition> leaderboardsPosition = new List<clsLeaderboardWithPosition>();
            for (int i = 1; i <= leaderboards.Count; i++)
            {
                leaderboardsPosition.Add(new clsLeaderboardWithPosition(leaderboards[i - 1], i));
            }

            //50 Mapas
            originalMapList = new ObservableCollection<clsMapLeaderboard>();
            originalMapList.Add(new clsMapLeaderboard(new clsMap(1, "Nick1", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(2, "Nick2", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(3, "Nick3", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(4, "Nick4", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(5, "Nick5", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(6, "Nick6", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(7, "Nick7", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(8, "Nick8", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(9, "Nick9", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(10, "Nick10", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(11, "Nick11", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(12, "Nick12", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(13, "Nick13", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(14, "Nick14", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(15, "Nick15", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(16, "Nick16", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(17, "Nick17", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(18, "Nick18", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(19, "Nick19", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(20, "Nick20", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick21", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(1, "Nick22", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(2, "Nick23", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(3, "Nick24", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(4, "Nick25", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(5, "Nick26", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(6, "Nick27", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(7, "Nick28", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(8, "Nick29", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(9, "Nick30", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(10, "Nick31", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(11, "Nick32", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(12, "Nick33", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(13, "Nick34", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(14, "Nick35", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(15, "Nick36", 24, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(16, "Nick37", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(17, "Nick38", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(18, "Nick39", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(19, "Nick40", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(20, "Nick41", 36, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick42", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick43", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick44", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick45", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick46", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick47", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick48", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick49", 16, true), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick50", 16, true), leaderboardsPosition));
        }
        private void crearMapasDePrueba2()
        {
            //No hace falta ordenar ya vendria de la base datos 
            List<clsLeaderboard> leaderboards = new List<clsLeaderboard>(getLeaderboards().OrderByDescending(leaderboard => leaderboard.Score)); //Order Leaderboards of mapaseled by Score
            List<clsLeaderboardWithPosition> leaderboardsPosition = new List<clsLeaderboardWithPosition>();
            for (int i = 1; i <= leaderboards.Count; i++)
            {
                leaderboardsPosition.Add(new clsLeaderboardWithPosition(leaderboards[i - 1], i));
            }

            //50 Mapas
            nextOriginalMapList = new ObservableCollection<clsMapLeaderboard>();
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(1, "Nick1", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(2, "Nick2", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(3, "Nick3", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(4, "Nick4", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(5, "Nick5", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(6, "Nick6", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(7, "Nick7", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(8, "Nick8", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(9, "Nick9", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(10, "Nick10", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(11, "Nick11", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(12, "Nick12", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(13, "Nick13", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(14, "Nick14", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(15, "Nick15", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(16, "Nick16", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(17, "Nick17", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(18, "Nick18", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(19, "Nick19", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(20, "Nick20", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick21", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(1, "Nick22", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(2, "Nick23", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(3, "Nick24", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(4, "Nick25", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(5, "Nick26", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(6, "Nick27", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(7, "Nick28", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(8, "Nick29", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(9, "Nick30", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(10, "Nick31", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(11, "Nick32", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(12, "Nick33", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(13, "Nick34", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(14, "Nick35", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(15, "Nick36", 24, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(16, "Nick37", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(17, "Nick38", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(18, "Nick39", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(19, "Nick40", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(20, "Nick41", 36, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick42", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick43", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick44", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick45", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick46", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick47", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick48", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick49", 16, true), leaderboardsPosition));
            nextOriginalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick50", 16, true), leaderboardsPosition));
        }
    }
}
