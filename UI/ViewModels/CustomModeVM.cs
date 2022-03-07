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
using Windows.UI.Popups;
using System.Data.SqlClient;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace UI.ViewModels
{
    public class CustomModeVM : clsVMBase
    {
        #region Attributes
        ObservableCollection<clsMapLeaderboardWithElements> originalMapList;//Lista que guarda 50 mapas
        ObservableCollection<clsMapLeaderboardWithElements> nextOriginalMapListRight;//Lista que guarda los siguientes mapas(Es necesario porque se añadira a originalMapList cuando se pase a los siguientes 50 mapas)
        ObservableCollection<clsMapLeaderboardWithElements> nextOriginalMapListLeft;//Lista que guarda los anteriores mapas(Es necesario porque se añadira a originalMapList cuando se pase a los anteriores 50 mapas)
        ObservableCollection<clsMapLeaderboardWithElements> mapList;//Lista que tendra los mapas que se ven actualmente, ira de 10 en 10
        clsMapLeaderboardWithElements mapSelected;
        List<clsElementMap> allElementMaps;
        List<clsLeaderboard> allLeaderboards;

        DelegateCommand leftFilterButtonCommand;
        DelegateCommand rightFilterButtonCommand;

        int posicionUltimoMapa = 0;//Necesario para controlar porque parte se va de la lista donde estan todos los mapas. Son lo las paginas de la lista
        int ultimoMapaObtenidoBBDD = 0;//Necesario para controlar a partir de que numero de mapa hay que obtener los mapas en la bbdd
        int primeraCargadaDeMapas = 0;//Atributo neceasio para controlar que la primera vez, no se pueda ir a la izquierda de la lista

        //Variables necesarias porque puede ser que el usuario llegue a la pagina donde se cargan los mapas, vuelva para las paginas anteriores, y luego llegar otra vez a la pagina en la que se cargan los mapas, pues con este booleano se pueda controlar que no se vuelva a cargar otra vez los mapas
        bool siguientesMapasCargados = false;
        bool anterioresMapasCargados = false;
        #endregion

        #region Builders
        public CustomModeVM()
        {
            SharedData.AllImageSourceOfSprites = fromByteToImageConverter.convertirByteImagen(clsElementTypeQueryBL.getAllSpritesBL());
            rightFilterButtonCommand = new DelegateCommand(RightFilterButtonCommand_Executed, RightFilterButtonCommand_CanExecuted);
            leftFilterButtonCommand = new DelegateCommand(LeftFilterButtonCommand_Executed, LeftFilterButtonCommand_CanExecuted);
            nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboardWithElements>();
            nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboardWithElements>();

            try
            {
                allLeaderboards = clsLeaderboardQueryBL.getAllLeaderboardBL();
                allElementMaps = clsElementMapQueryBL.getListOfElementMapBL();

                List<clsMapLeaderboardWithElements> clsMapLeaderboardWithElements = new List<clsMapLeaderboardWithElements>();
                List<clsElementMap> specificMapElements = null;
                foreach (clsMap map in clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "@NumeroElementos AND @NumeroElementos +50"))
                {
                    specificMapElements = new List<clsElementMap>(from element in allElementMaps
                                                                  where element.IdMap == map.Id
                                                                  select element);
                    for (int i = 0; i < specificMapElements.Count; i++) //Dividirlo por que las posicion estan en un sitio para un canvas y en otro para un grid
                    {
                        specificMapElements[i].AxisX /= 50;
                        specificMapElements[i].AxisY /= 50;
                    }
                    clsMapLeaderboardWithElements.Add(new clsMapLeaderboardWithElements(map, getLeaderboardsOfMap(map.Id), specificMapElements)); //Hacer metodo generico para lo de las puntuaciones 
                }
                originalMapList = new ObservableCollection<clsMapLeaderboardWithElements>(clsMapLeaderboardWithElements);
                ultimoMapaObtenidoBBDD = 51; //Para que se puedan obtener mapas de 50 en 50
                cargarMapasPosicionEspecificada(posicionUltimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
            catch (SqlException)
            {
                Utilidades.mostrarMensajeAsync("Ha ocurrido un error al obtener los mapas");
            }
            catch (Exception)
            {
                showError();
            }
        }
        #endregion

        #region Getters & Setters
        public ObservableCollection<clsMapLeaderboardWithElements> MapList { get => mapList; }
        public clsMapLeaderboardWithElements MapSelected
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
        #endregion

        #region Commands
        public DelegateCommand LeftFilterButtonCommand
        {
            get { return leftFilterButtonCommand; }
        }
        /// <summary>
        ///     <header>private async void LeftFilterButtonCommand_Executed()</header>
        ///     <description>This command controls the left click to move between pages</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Move to the left</postcondition>
        /// </summary>
        private async void LeftFilterButtonCommand_Executed()
        {
            posicionUltimoMapa -= 10;
            if (posicionUltimoMapa == -10)//Corresponde a cuando se esta en la 5 pagina y se hace click entonces de 1 pasa a 5 pero de los mapas anteriores y por lo tanto se hace un reset 
            {
                if (!anterioresMapasCargados && ultimoMapaObtenidoBBDD - 50 > 0)
                {
                    anterioresMapasCargados = true;
                    try
                    {
                        List<clsMap> listaMapasSiguientes = await Task.Run(() => { return clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "@NumeroElementos - 100 AND @NumeroElementos -51"); });
                        if (listaMapasSiguientes.Count > 0)
                        {
                            List<clsMapLeaderboardWithElements> listaMapasSiguientesConPuntuacion = new List<clsMapLeaderboardWithElements>();
                            foreach (clsMap map in listaMapasSiguientes)
                            {
                                listaMapasSiguientesConPuntuacion.Add(
                                    new clsMapLeaderboardWithElements(map, getLeaderboardsOfMap(map.Id))); 
                            }
                            nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboardWithElements>(listaMapasSiguientesConPuntuacion);
                            ultimoMapaObtenidoBBDD -= 50;
                        }
                    }
                    catch (SqlException)
                    {
                        Utilidades.mostrarMensajeAsync("Ocurrio un error al obtener los mapas");
                    }
                }
                anterioresMapasCargados = false;
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                posicionUltimoMapa = 40;
                primeraCargadaDeMapas--;
                nextOriginalMapListRight = originalMapList;
                originalMapList = nextOriginalMapListLeft;
                cargarMapasPosicionEspecificada(posicionUltimoMapa);
                leftFilterButtonCommand.RaiseCanExecuteChanged();
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
            else
            {
                cargarMapasPosicionEspecificada(posicionUltimoMapa);
                leftFilterButtonCommand.RaiseCanExecuteChanged();
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        ///     <header>private bool LeftFilterButtonCommand_CanExecuted()</header>
        ///     <description>This method makes the left button executable or not, dpends on the maps in the db</description>
        ///     <precondition>None</precondition>ç
        ///     <postcondition>Makes the button executable or not</postcondition>
        /// </summary>
        /// <returns>bool executable</returns>
        private bool LeftFilterButtonCommand_CanExecuted()
        {
            bool desactivarCommand = true;
            if (
                ((posicionUltimoMapa == 0) && primeraCargadaDeMapas == 0) ||
                (posicionUltimoMapa - 10 == -10 && nextOriginalMapListLeft.Count == 0))
            {
                desactivarCommand = false;
            }

            return desactivarCommand;
        }

        //rightFilterButtonCommand
        public DelegateCommand RightFilterButtonCommand
        {
            get { return rightFilterButtonCommand; }
        }
        /// <summary>
        ///     <header>private async void RightFilterButtonCommand_Executed()</header>
        ///     <description>This command controls the right click to move between pages</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Move to the right</postcondition>
        /// </summary>
        private async void RightFilterButtonCommand_Executed()
        {
            posicionUltimoMapa += 10;
            if (posicionUltimoMapa == 50)//Corresponde a cuando se esta en la 5 pagina y se hace click entonces de 40 pasa a 50 y por lo tanto se hace un reset 
            {
                ultimoMapaObtenidoBBDD += 50;
                siguientesMapasCargados = false;
                primeraCargadaDeMapas++;
                nextOriginalMapListLeft = originalMapList;
                posicionUltimoMapa = 0;
                originalMapList = nextOriginalMapListRight;
                cargarMapasPosicionEspecificada(posicionUltimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
            else
            {
                if (posicionUltimoMapa == 30 && !siguientesMapasCargados) //Corresponde a cuando se este pasando de la pagina 3 y la siguiente en cargar sea la 4
                {
                    siguientesMapasCargados = true;
                    try
                    {
                        List<clsMap> listaMapasSiguientes = await Task.Run(() => { return clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "@NumeroElementos AND @NumeroElementos +50"); });
                        if (listaMapasSiguientes.Count > 0)
                        {
                            List<clsMapLeaderboardWithElements> listaMapasSiguientesConPuntuacion = new List<clsMapLeaderboardWithElements>();
                            foreach (clsMap map in listaMapasSiguientes)
                            {

                                listaMapasSiguientesConPuntuacion.Add(
                                    new clsMapLeaderboardWithElements(map, getLeaderboardsOfMap(map.Id)));
                            }
                            nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboardWithElements>(listaMapasSiguientesConPuntuacion);
                        }
                    }
                    catch (SqlException)
                    {
                        Utilidades.mostrarMensajeAsync("Ocurrio un error al obtener los mapas");
                    }
                }
                cargarMapasPosicionEspecificada(posicionUltimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                leftFilterButtonCommand.RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        ///     <header>private bool RightFilterButtonCommand_CanExecuted()</header>
        ///     <description>This method makes the right button executable or not, dpends on the maps in the db</description>
        ///     <precondition>None</precondition>ç
        ///     <postcondition>Makes the button executable or not</postcondition>
        /// </summary>
        /// <returns>bool executable</returns>
        private bool RightFilterButtonCommand_CanExecuted()
        {
            bool activarCommand = true;
            if (mapList != null && mapList.Count < 10 || (posicionUltimoMapa == 40 && nextOriginalMapListRight.Count == 0)
                || (posicionUltimoMapa != 40 && posicionUltimoMapa >= originalMapList.Count - 10))
            {
                activarCommand = false;
            }
            return activarCommand;
        }
        #endregion

        #region Methods
        /// <summary>
        /// <header>private void cargarMapasPosicionEspecificada(int posicion)</header>
        /// <description> This method changes the list of maps that are showed</description>
        ///<precondition>position must be bigger than 0</precondition> 
        /// <postcondition> List of maps is updated.</postcondition>
        /// </summary>
        /// <param name="posicion">int</param>
        private void cargarMapasPosicionEspecificada(int posicion)
        {
            int numeroDeSiguientesMapas = 10;
            if ((posicion + 10) >= originalMapList.Count - 1)
            {
                numeroDeSiguientesMapas = originalMapList.Count - posicion;
            }
            mapList = new ObservableCollection<clsMapLeaderboardWithElements>
                    (originalMapList.ToList().GetRange(posicion, numeroDeSiguientesMapas));
            NotifyPropertyChanged("MapList");

            if (mapList.Count != 0) 
            {
                mapSelected = mapList[0];
                NotifyPropertyChanged("MapSelected");
            }
        }
        /// <summary>
        ///     <header>private List<clsLeaderboard> getLeaderboardsOfMap(int idMap)</header>
        ///     <description>This method gets the leaderboard from the db</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Returns the list of leaderboards from a map</postcondition>
        /// </summary>
        /// <param name="idMap">int</param>
        /// <returns>List<clsLeaderboard> leaderboards</returns>
        private List<clsLeaderboard> getLeaderboardsOfMap(int idMap)
        {
            List<clsLeaderboard> leaderboardsOfMap;
            leaderboardsOfMap = new List<clsLeaderboard>(from leaderboard in allLeaderboards
                                                         where leaderboard.IdMap == idMap
                                                         select leaderboard);
            int numeroLeadersBoards = leaderboardsOfMap.Count;

            if (numeroLeadersBoards > 10) { 
                numeroLeadersBoards = 11;
            }
            leaderboardsOfMap = leaderboardsOfMap.GetRange(0, numeroLeadersBoards); //Obtener solo los 10 primeros
            return leaderboardsOfMap;
        }
        #endregion
    }
}
