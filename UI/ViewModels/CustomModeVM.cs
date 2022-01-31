using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using UI.Models;
using UI.ViewModels.Utilities;

namespace UI.ViewModels
{
    public class CustomModeVM : clsVMBase
    {
        //CUANDO SE VAYA A CARGAR LOS SIGUIENTES MAPAS DE LA BASE DE DATOS, HACERLO ASYNC PARA QUE EL USUARIO SIGA INTERACTUANDO CON LA LISTA MIENTRAS SE CARGAN LOS SIGUIENTES MAPAS
        #region Attributes
        ObservableCollection<clsMapLeaderboard> originalMapList;
        ObservableCollection<clsMapLeaderboard> mapList;
        clsMapLeaderboard mapSelected;
        string inputText;
        DelegateCommand leftFilterButtonCommand;
        DelegateCommand rightFilterButtonCommand;
        int posicion;
        #endregion

        #region Builders
        public CustomModeVM()
        {
            posicion = 1;
            crearMapasDePrueba();
            mapList = originalMapList;
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
                //TOOD HACER QUE LA LISTA VENGA YA ORDENADA DE LA BBDD
                //mapSelected.Leaderboards = new List<clsLeaderboardWithPosition>(mapSelected.Leaderboards.OrderByDescending(leaderboard => leaderboard.Position)); //Order Leaderboards of mapaseled by Score
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
                }
                else
                {
                    filterList();
                }
            }
        }
        #endregion
        public int Posicion { get { return posicion++; } }
        #region Commands
        public DelegateCommand LeftFilterButtonCommand
        {
            get { return leftFilterButtonCommand = new DelegateCommand(LeftFilterButtonCommand_Executed, LeftFilterButtonCommand_CanExecuted); }
        }

        private void LeftFilterButtonCommand_Executed()
        {
            //TODO volver a los anteriores 10 mapas
        }

        private bool LeftFilterButtonCommand_CanExecuted()
        {
            //TODO Controlar cuando se llegue al limite por la izquierda
            return true;
        }

        public DelegateCommand RightFilterButtonCommand
        {
            get { return rightFilterButtonCommand = new DelegateCommand(RightFilterButtonCommand_Executed, RightFilterButtonCommand_CanExecuted); }
        }

        private void RightFilterButtonCommand_Executed()
        {
            //TODO Cargar siguientes 10 mapas
        }

        private bool RightFilterButtonCommand_CanExecuted()
        {
            //TODO Controlar cuando se llegue al limite por la Derecha
            return true;
        }
        #endregion

        #region Methods 
        private void crearMapasDePrueba()
        {
            //No hace falta ordenar ya vendria de la base datos 
            List<clsLeaderboard> leaderboards = new List<clsLeaderboard>(getLeaderboards().OrderByDescending(leaderboard => leaderboard.Score)); //Order Leaderboards of mapaseled by Score
            List<clsLeaderboardWithPosition> leaderboardsPosition = new List<clsLeaderboardWithPosition>();
            for (int i = 1; i <= leaderboards.Count; i++) {
                leaderboardsPosition.Add(new clsLeaderboardWithPosition(leaderboards[i-1],i));
            }

            //21 Mapas
            originalMapList = new ObservableCollection<clsMapLeaderboard>();
            originalMapList.Add(new clsMapLeaderboard(new clsMap(1, "Nick1", 16, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(2, "Nick2", 24, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(3, "Nick3", 36, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(4, "Nick4", 24, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(5, "Nick5", 36, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(6, "Nick6", 24, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(7, "Nick7", 16, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(8, "Nick8", 24, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(9, "Nick9", 36, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(10, "Nick10", 16, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(11, "Nick11", 24, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(12, "Nick12", 16, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(13, "Nick13", 24, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(14, "Nick14", 16, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(15, "Nick15", 24, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(16, "Nick16", 16, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(17, "Nick17", 36, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(18, "Nick18", 16, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(19, "Nick19", 36, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(20, "Nick20", 36, 1), leaderboardsPosition));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick21", 16, 1), leaderboardsPosition));
        }
        private void filterList()
        {
            mapList = new ObservableCollection<clsMapLeaderboard>(from map in originalMapList
                                                                  where map.Nick.ToLower() == inputText.ToLower()
                                                                  select map);
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
    }
}
