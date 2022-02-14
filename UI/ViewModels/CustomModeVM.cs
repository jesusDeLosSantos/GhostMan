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
      
        bool siguientesMapasCargados = false;
        bool anterioresMapasCargados = false;
        #endregion


        int x = 0;
        //Eliminar ,prueba para la view PruebaMoverCanvas
        public int X { get { return x; } set { x = value; NotifyPropertyChanged("X"); } }  
  











        #region Builders
        public CustomModeVM()
        {
            rightFilterButtonCommand = new DelegateCommand(RightFilterButtonCommand_Executed, RightFilterButtonCommand_CanExecuted);
            leftFilterButtonCommand = new DelegateCommand(LeftFilterButtonCommand_Executed, LeftFilterButtonCommand_CanExecuted);
            nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboard>();
            nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboard>();

            try
            {
                List<clsMapLeaderboard> b = new List<clsMapLeaderboard>();
                foreach (clsMap map in clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "@NumeroElementos AND @NumeroElementos +50"))
                {
                    b.Add(new clsMapLeaderboard(map, new List<clsLeaderboardWithPosition>()));
                }
                originalMapList = new ObservableCollection<clsMapLeaderboard>(b);
                ultimoMapaObtenidoBBDD += originalMapList.Count;
                ultimoMapaObtenidoBBDD = 51;
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
            catch (SqlException)
            {
                mostrarMensajeAsync("Ocurrio un error al obtener los mapas");
            }
            catch (Exception)
            {
                mostrarMensajeAsync("A ocurrido un error desconocido.");
            }
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

                posicionUtlimoMapa = 0;
                ultimoMapaObtenidoBBDD = 0;

                siguientesMapasCargados = false;
                anterioresMapasCargados = false;
                primeraCargada = 0;
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

            posicionUtlimoMapa -= 10;
            if (posicionUtlimoMapa == -10)//Corresponde a cuando se esta en la 5 pagina y se hace click entonces de 1 pasa a 5 pero de los mapas anteriores y por lo tanto se hace un reset 
            {
                if (!anterioresMapasCargados && ultimoMapaObtenidoBBDD - 50 > 0) {
                    anterioresMapasCargados = true;
                    try
                    {
                        List<clsMap> listaMapasSiguientes = await Task.Run(() => { return clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "@NumeroElementos - 100 AND @NumeroElementos -51"); });
                        if (listaMapasSiguientes.Count > 0)
                        {
                            List<clsMapLeaderboard> listaMapasSiguientesConPuntuacion = new List<clsMapLeaderboard>();
                            foreach (clsMap map in listaMapasSiguientes)
                            {
                                listaMapasSiguientesConPuntuacion.Add(
                                    new clsMapLeaderboard(map, new List<clsLeaderboardWithPosition>()));
                            }
                            nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboard>(listaMapasSiguientesConPuntuacion);
                            ultimoMapaObtenidoBBDD -= 50;
                        }/*
                        else
                        { //Si la lista de mapas que se obtienen de la bbdd no hay mas mapas se resetea nextOriginalMapList
                            nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboard>();
                        }*/
                    }
                    catch (SqlException)
                    {
                        mostrarMensajeAsync("Ocurrio un error al obtener los mapas");
                    }
                }
                anterioresMapasCargados = false;
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                posicionUtlimoMapa = 40;
                primeraCargada--;
                nextOriginalMapListRight = originalMapList;
                originalMapList = nextOriginalMapListLeft;
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);
                leftFilterButtonCommand.RaiseCanExecuteChanged();
                rightFilterButtonCommand.RaiseCanExecuteChanged();
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

            if (
                ((posicionUtlimoMapa == 0) && primeraCargada == 0) ||
                (posicionUtlimoMapa - 10 == -10 && nextOriginalMapListLeft.Count == 0))
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

        private async void RightFilterButtonCommand_Executed()
        {
            posicionUtlimoMapa += 10;
            if (posicionUtlimoMapa == 50)//Corresponde a cuando se esta en la 5 pagina y se hace click entonces de 40 pasa a 50 y por lo tanto se hace un reset 
            {

                ultimoMapaObtenidoBBDD += 50;
                siguientesMapasCargados = false;
                primeraCargada++;
                nextOriginalMapListLeft = originalMapList;
                posicionUtlimoMapa = 0;
                originalMapList = nextOriginalMapListRight; 
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
            }
            else
            {
                if (posicionUtlimoMapa == 30 && !siguientesMapasCargados) //Corresponde a cuando se este pasando de la pagina 3 y la siguiente en cargar sea la 4
                {
                    siguientesMapasCargados = true;
                    try
                    {
                        List<clsMap> listaMapasSiguientes = await Task.Run(() => { return clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "@NumeroElementos AND @NumeroElementos +50"); });
                        if (listaMapasSiguientes.Count > 0)
                        {
                            List<clsMapLeaderboard> listaMapasSiguientesConPuntuacion = new List<clsMapLeaderboard>();
                            foreach (clsMap map in listaMapasSiguientes)
                            {
                                listaMapasSiguientesConPuntuacion.Add(
                                    new clsMapLeaderboard(map, new List<clsLeaderboardWithPosition>()));
                            }
                            nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboard>(listaMapasSiguientesConPuntuacion);
                        }
                    }
                    catch (SqlException)
                    {
                        mostrarMensajeAsync("Ocurrio un error al obtener los mapas");
                    }
                }
                cargarMapasPosicionEspecificada(posicionUtlimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                leftFilterButtonCommand.RaiseCanExecuteChanged();
            }
        }

        private bool RightFilterButtonCommand_CanExecuted()
        {
            bool activarCommand = true;
            if (mapList.Count < 10 || (posicionUtlimoMapa == 40 && nextOriginalMapListRight.Count == 0)
                || (posicionUtlimoMapa != 40 && posicionUtlimoMapa >= originalMapList.Count - 10))
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

        private void filterList()
        {
            mapList = new ObservableCollection<clsMapLeaderboard>(from map in originalMapList
                                                                  where map.Nick.ToLower().Contains(inputText.ToLower())
                                                                  select map);
            NotifyPropertyChanged("MapList");
        }

        /// <summary>
        /// Cabecera: private async void mostrarMensajeAsync(string mensaje)
        /// Comentario: Este metodo se encarga de mostrar un MessageDialog con un mensaje que tendra una opcion de cerrar.
        /// Entradas: string mensaje
        /// Salidas: Ninguna
        /// Precondiciones: Ninguna
        /// Postcondiciones: Se mostrara un mensaje al usuario en un MessageDialog, que contentra una opcion de cerrar.
        /// </summary>
        /// <param name="mensaje"></param>
        private async void mostrarMensajeAsync(string mensaje)
        {
            var dialog = new MessageDialog(mensaje);
            await dialog.ShowAsync();
        }
        #endregion
    }
}
