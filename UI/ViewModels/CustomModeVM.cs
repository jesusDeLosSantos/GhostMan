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
        ObservableCollection<clsMapLeaderboard> nextOriginalMapListRight;
        ObservableCollection<clsMapLeaderboard> nextOriginalMapListLeft;
        ObservableCollection<clsMapLeaderboard> mapList;
        clsMapLeaderboard mapSelected;
        string inputText;
        DelegateCommand leftFilterButtonCommand;
        DelegateCommand rightFilterButtonCommand;
        int posicionUtlimoMapa = 0;
        int ultimoMapaObtenidoBBDD = 0;
        int primeraCargada = 0;
        #endregion

        #region Builders
        public CustomModeVM()
        {
            rightFilterButtonCommand = new DelegateCommand(RightFilterButtonCommand_Executed, RightFilterButtonCommand_CanExecuted);
            leftFilterButtonCommand = new DelegateCommand(LeftFilterButtonCommand_Executed, LeftFilterButtonCommand_CanExecuted);
            nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboard>();
            nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboard>();

            List<clsMapLeaderboard> b = new List<clsMapLeaderboard>();
            foreach (clsMap map in clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "+ 50"))
            {
                b.Add(new clsMapLeaderboard(map, new List<clsLeaderboardWithPosition>()));
            }
            originalMapList = new ObservableCollection<clsMapLeaderboard>(b);
            ultimoMapaObtenidoBBDD += originalMapList.Count;

            cargarMapasPosicionEspecificada(posicionUtlimoMapa);
            rightFilterButtonCommand.RaiseCanExecuteChanged();
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

        private async void LeftFilterButtonCommand_Executed()
        {
            if (posicionUtlimoMapa == 0) //Corresponde a la 3 Pagina
            {
                primeraCargada--;
                //nextOriginalMapListLeft = nextOriginalMapListRight;

                List<clsMap> listaMapasSiguientes = await Task.Run(() => { return clsMapQueryBL.getEspecificNumbersCustomMapsDALLEFT(ultimoMapaObtenidoBBDD-originalMapList.Count, "-50"); });
                if (listaMapasSiguientes.Count > 0)
                {
                    List<clsMapLeaderboard> listaMapasSiguientesConPuntuacion = new List<clsMapLeaderboard>();
                    foreach (clsMap map in listaMapasSiguientes)
                    {
                        listaMapasSiguientesConPuntuacion.Add(
                            new clsMapLeaderboard(map, new List<clsLeaderboardWithPosition>()));
                    }
                    nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboard>(listaMapasSiguientesConPuntuacion);
                    //ultimoMapaObtenidoBBDD -= listaMapasSiguientes.Count;
                }
                else
                { //Si la lista de mapas que se obtienen de la bbdd no hay mas mapas se resetea nextOriginalMapList
                    nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboard>();
                }
            }
            posicionUtlimoMapa -= 10;
            if (posicionUtlimoMapa == -10)//Corresponde a cuando se esta en la 5 pagina y se hace click entonces de 40 pasa a 50 y por lo tanto se hace un reset 
            {
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                posicionUtlimoMapa = 40;
                originalMapList = nextOriginalMapListLeft; //Se llama a la bbdd entonces la condicion del comand es si la bbdd la lista de mapas que me da tiene elementos
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);//Es te eliminarlo cuando se ponga el if comentado;
                leftFilterButtonCommand.RaiseCanExecuteChanged();
            }
            else
            {
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);
                leftFilterButtonCommand.RaiseCanExecuteChanged();
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
        }

        private bool LeftFilterButtonCommand_CanExecuted()
        {
            bool desactivarCommand = true;

            if (((posicionUtlimoMapa-10 == -10) && primeraCargada == 0) || (posicionUtlimoMapa-10 == -10 && nextOriginalMapListLeft.Count == 0))//|| posicionUtlimoMapa + 10 >= originalMapList.Count) //|| posicionUtlimoMapa == originalMapList.Count)
            {
                desactivarCommand = false;
            }

            return desactivarCommand;
        }

        public DelegateCommand RightFilterButtonCommand
        {
            get { return rightFilterButtonCommand; }
        }

        private async void RightFilterButtonCommand_Executed()
        {
            posicionUtlimoMapa += 10;
            if (posicionUtlimoMapa == 50)//Corresponde a cuando se esta en la 5 pagina y se hace click entonces de 40 pasa a 50 y por lo tanto se hace un reset 
            {
                primeraCargada++;
                nextOriginalMapListLeft = originalMapList;
                posicionUtlimoMapa = 0;
                originalMapList = nextOriginalMapListRight; //Se llama a la bbdd entonces la condicion del comand es si la bbdd la lista de mapas que me da tiene elementos
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);//Es te eliminarlo cuando se ponga el if comentado;
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
            else
            {
                if (posicionUtlimoMapa == 30) //Corresponde a la 3 Pagina
                {
                    
                    List<clsMap> listaMapasSiguientes = await Task.Run(() => { return clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD+1, "+ 50"); });
                    if (listaMapasSiguientes.Count > 0)
                    {
                        List<clsMapLeaderboard> listaMapasSiguientesConPuntuacion = new List<clsMapLeaderboard>();
                        foreach (clsMap map in listaMapasSiguientes)
                        {
                            listaMapasSiguientesConPuntuacion.Add(
                                new clsMapLeaderboard(map, new List<clsLeaderboardWithPosition>()));
                        }
                        nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboard>(listaMapasSiguientesConPuntuacion);
                        ultimoMapaObtenidoBBDD += listaMapasSiguientes.Count;
                    }/*
                    else
                    { //Si la lista de mapas que se obtienen de la bbdd no hay mas mapas se resetea nextOriginalMapList
                        nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboard>();
                    }*/
                }
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                leftFilterButtonCommand.RaiseCanExecuteChanged();
            }
        }

        private bool RightFilterButtonCommand_CanExecuted()
        {
            bool activarCommand = true;
            if (mapList.Count < 10 || (posicionUtlimoMapa == 40 && nextOriginalMapListRight.Count == 0) || (primeraCargada != 0 && posicionUtlimoMapa + 10 >= nextOriginalMapListRight.Count))
            {
                activarCommand = false;
            }
            return activarCommand;
        }
        #endregion

        #region Methods 
        private void cargarMapasPosicionEspecificada(int posicion)
        {
            int numeroDeSiguientesMapas = 10;
            if ((posicion + 10) >= originalMapList.Count - 1)
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

        public List<clsLeaderboard> getLeaderboards()
        {
            List<clsLeaderboard> list = new List<clsLeaderboard>();
            list.Add(new clsLeaderboard(1, "Jugador 1", 1111111111));
            list.Add(new clsLeaderboard(1, "Jugador 2", 2));
            list.Add(new clsLeaderboard(1, "Jugador 3", 0));
            list.Add(new clsLeaderboard(1, "Jugador 4", 1));
            list.Add(new clsLeaderboard(1, "Jugador 5", 10));
            list.Add(new clsLeaderboard(1, "Jugador 6", 1000));
            list.Add(new clsLeaderboard(1, "Jugador 1", 1111111111));
            list.Add(new clsLeaderboard(1, "Jugador 2", 2));
            list.Add(new clsLeaderboard(1, "Jugador 3", 0));
            list.Add(new clsLeaderboard(1, "Jugador 4", 1));
            return list;
        }
        #endregion
        /*
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
        }*/
    }
}
