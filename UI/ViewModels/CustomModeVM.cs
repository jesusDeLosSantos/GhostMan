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
        #region Attributes
        ObservableCollection<clsMapLeaderboard> originalMapList;//Lista que guarda 50 mapas
        ObservableCollection<clsMapLeaderboard> nextOriginalMapListRight;//Lista que guarda los siguientes mapas(Es necesario porque se añadira a originalMapList cuando se pase a los siguientes 50 mapas)
        ObservableCollection<clsMapLeaderboard> nextOriginalMapListLeft;//Lista que guarda los anteriores mapas(Es necesario porque se añadira a originalMapList cuando se pase a los anteriores 50 mapas)
        ObservableCollection<clsMapLeaderboard> mapList;//Lista que tendra los mapas que se ven actualmente, ira de 10 en 10
        clsMapLeaderboard mapSelected;
        //string inputText;

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
            rightFilterButtonCommand = new DelegateCommand(RightFilterButtonCommand_Executed, RightFilterButtonCommand_CanExecuted);
            leftFilterButtonCommand = new DelegateCommand(LeftFilterButtonCommand_Executed, LeftFilterButtonCommand_CanExecuted);
            nextOriginalMapListRight = new ObservableCollection<clsMapLeaderboard>();
            nextOriginalMapListLeft = new ObservableCollection<clsMapLeaderboard>();

            try
            {
                List<clsMapLeaderboard> b = new List<clsMapLeaderboard>();
                foreach (clsMap map in clsMapQueryBL.getEspecificNumbersCustomMapsDAL(ultimoMapaObtenidoBBDD, "@NumeroElementos AND @NumeroElementos +50"))
                {
                    b.Add(new clsMapLeaderboard(map, new List<clsLeaderboardWithPosition>())); //Hacer metodo generico para lo de las puntuaciones 
                }
                originalMapList = new ObservableCollection<clsMapLeaderboard>(b);
                ultimoMapaObtenidoBBDD = 51;
                cargarMapasPosicionEspecificada(posicionUltimoMapa);
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
        /*public string InputText
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
        }*/
        #endregion

        #region Commands
        public DelegateCommand LeftFilterButtonCommand
        {
            get { return leftFilterButtonCommand; }
        }

        private async void LeftFilterButtonCommand_Executed()
        {
            posicionUltimoMapa -= 10;
            if (posicionUltimoMapa == -10)//Corresponde a cuando se esta en la 5 pagina y se hace click entonces de 1 pasa a 5 pero de los mapas anteriores y por lo tanto se hace un reset 
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
                        }
                    }
                    catch (SqlException)
                    {
                        mostrarMensajeAsync("Ocurrio un error al obtener los mapas");
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
                cargarMapasPosicionEspecificada(posicionUltimoMapa);
                rightFilterButtonCommand.RaiseCanExecuteChanged();
                leftFilterButtonCommand.RaiseCanExecuteChanged();
            }
        }

        private bool RightFilterButtonCommand_CanExecuted()
        {
            bool activarCommand = true;
            if (mapList.Count < 10 || (posicionUltimoMapa == 40 && nextOriginalMapListRight.Count == 0)
                || (posicionUltimoMapa != 40 && posicionUltimoMapa >= originalMapList.Count - 10))
            {
                activarCommand = false;
            }
            return activarCommand;
        }
        #endregion

        /// <summary>
        /// Caebecera: private void cargarMapasPosicionEspecificada(int posicion)
        /// Comentario: Este metodo se encarga de modificar la lista donde estan los mapas que se estan mostrando.
        /// Entradas: int posicion
        /// Salidas: Ninguna
        /// Precondiciones: posicion tiene que ser mayor que 0 
        /// Postcondiciones: Se actualizara el contenido de la lista de mapas que se muestran actualmente.
        ///                  Dicha actualizacion se hara cogiendo los 10 mapas siguientes que hay en otra lista.
        /// </summary>
        /// <param name="posicion"></param>
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

            if (mapList.Count != 0) //Debugear y ver si es necesario 
            {
                mapSelected = mapList[0];
                NotifyPropertyChanged("MapSelected");
            }
        }
        /*
        private void filterList()
        {
            mapList = new ObservableCollection<clsMapLeaderboard>(from map in originalMapList
                                                                  where map.Nick.ToLower().Contains(inputText.ToLower())
                                                                  select map);
            NotifyPropertyChanged("MapList");
        }
        */

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
