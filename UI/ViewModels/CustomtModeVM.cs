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
    public class CustomtModeVM : clsVMBase
    {
        #region Attributes
        ObservableCollection<clsMapLeaderboard> originalMapList;
        ObservableCollection<clsMapLeaderboard> mapList;
        clsMapLeaderboard mapSelected;
        string inputText;
        DelegateCommand leftFilterButtonCommand;
        DelegateCommand rightFilterButtonCommand;
        #endregion

        #region Builders
        public CustomtModeVM()
        {
            mapList = originalMapList;
        }
        #endregion

        #region Getters & Setters
        public ObservableCollection<clsMapLeaderboard> MapList { get => mapList; }
        public clsMapLeaderboard MapSelected { get => mapSelected; }
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

                if (string.IsNullOrEmpty(value)) { //Si se borra todo el contenido del input text
                    mapList = originalMapList;
                }
                else {
                    filterList();
                }
            }
        }
        #endregion

        #region Commands
        public DelegateCommand LeftFilterButtonCommand
        {
            get { return leftFilterButtonCommand = new DelegateCommand(LeftFilterButtonCommand_Executed, LeftFilterButtonCommand_CanExecuted); }
        }

        private void LeftFilterButtonCommand_Executed() { 
        
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

        }

        private bool RightFilterButtonCommand_CanExecuted()
        {
            //TODO Controlar cuando se llegue al limite por la Derecha
            return true;
        }
        #endregion

        #region Methods 
        private void crearMapasDePrueba() {
            //21 Mapas
            originalMapList = new ObservableCollection<clsMapLeaderboard>();
            originalMapList.Add(new clsMapLeaderboard(new clsMap(1,"Nick1",1,1),new clsLeaderboard(1,"LeaderBoard1",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(2,"Nick2",1,1),new clsLeaderboard(2,"LeaderBoard2",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(3,"Nick3",1,1),new clsLeaderboard(3,"LeaderBoard3",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(4,"Nick4",1,1),new clsLeaderboard(4,"LeaderBoard4",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(5,"Nick5",1,1),new clsLeaderboard(5,"LeaderBoard5",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(6,"Nick6",1,1),new clsLeaderboard(6,"LeaderBoard6",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(7,"Nick7",1,1),new clsLeaderboard(7,"LeaderBoard7",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(8,"Nick8",1,1),new clsLeaderboard(8,"LeaderBoard8",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(9,"Nick9",1,1),new clsLeaderboard(9,"LeaderBoard9",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(10,"Nick10",1,1),new clsLeaderboard(10,"LeaderBoard10",100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(11, "Nick11", 1, 1), new clsLeaderboard(11, "LeaderBoard11", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(12, "Nick12", 1, 1), new clsLeaderboard(12, "LeaderBoard12", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(13, "Nick13", 1, 1), new clsLeaderboard(13, "LeaderBoard13", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(14, "Nick14", 1, 1), new clsLeaderboard(14, "LeaderBoard14", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(15, "Nick15", 1, 1), new clsLeaderboard(15, "LeaderBoard15", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(16, "Nick16", 1, 1), new clsLeaderboard(16, "LeaderBoard16", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(17, "Nick17", 1, 1), new clsLeaderboard(17, "LeaderBoard17", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(18, "Nick18", 1, 1), new clsLeaderboard(18, "LeaderBoard18", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(19, "Nick19", 1, 1), new clsLeaderboard(19, "LeaderBoard19", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(20, "Nick20", 1, 1), new clsLeaderboard(20, "LeaderBoard20", 100)));
            originalMapList.Add(new clsMapLeaderboard(new clsMap(21, "Nick21", 1, 1), new clsLeaderboard(21, "LeaderBoard21", 100)));
        }
        private void filterList() {
            mapList = new ObservableCollection<clsMapLeaderboard>(from map in originalMapList
                                                                   where map.Nick.ToLower() == inputText.ToLower()
                                                                   select map);
        }
        #endregion
    }
}
