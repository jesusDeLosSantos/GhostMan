using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BL.manager;
using BL.query;
using Entities;
using UI.Models;
using ViewModels.utilities;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace UI.ViewModels
{
    public class MapCreatorVM : clsVMBase
    {
        #region Attributes
        String mapName;
        String mapNick;
        List<clsElementType> elements;
        List<clsElementTypeSprite> elementsSprite;
        List<clsElementMap> fullMap;
        clsElementTypeSprite selectedElement = new clsElementTypeSprite();
        int size = 1500;
        DelegateCommand commandSaveMap;
        DelegateCommand commandSizeChangeToLittle;
        DelegateCommand commandSizeChangeToMedium;
        DelegateCommand commandSizeChangeToBig;
        ImageSource spriteSelected;
        Visibility visibility = Visibility.Collapsed;
        #endregion

        #region Getters y Setters
        public List<clsElementType> Elements { get => elements; set => elements = value; }
        public List<clsElementMap> FullMap { get => fullMap; set => fullMap = value; }
        public clsElementTypeSprite SelectedElement {
            get { return selectedElement; }
            set { 
                selectedElement = value;
                NotifyPropertyChanged("SelectedElement");
            }
        }
        public DelegateCommand CommandSaveMap
        {
            get
            {
                commandSaveMap = new DelegateCommand(SaveMapCommand_Execute, SaveMapCommand_CanExecute);
                NotifyPropertyChanged("CommandGuardar");
                return commandSaveMap;
            }
        }
        public ImageSource SpriteSelected { get => spriteSelected; set => spriteSelected = value; }
        public List <clsElementTypeSprite> ElementsSprite { get => elementsSprite; set => elementsSprite = value; }
        public int Size { get => size; set => size = value; }
        public DelegateCommand CommandSizeChangeToLittle 
        { 
            get
            {
                commandSizeChangeToLittle = new DelegateCommand(LittleSizeCommand_Execute);
                NotifyPropertyChanged("CommandSizeChangeToLittle");
                return commandSizeChangeToLittle;
            } 
        }
        public DelegateCommand CommandSizeChangeToMedium { 
            get
            {
                commandSizeChangeToMedium = new DelegateCommand(MediumSizeCommand_Execute);
                NotifyPropertyChanged("CommandSizeChangeToMedium");
                return commandSizeChangeToMedium;
            }
        }
        public DelegateCommand CommandSizeChangeToBig { 
            get 
            { 
                commandSizeChangeToBig = new DelegateCommand(BigSizeCommand_Execute);
                NotifyPropertyChanged("CommandSizeChangeToBig");
                return commandSizeChangeToBig;
            } 
        }
        public Visibility Visibility { get => visibility; set => visibility = value; }
        public string MapName
        {
            get
            {
                return mapName;
            }
            set
            {
                mapName = value;
                commandSaveMap.RaiseCanExecuteChanged();
                NotifyPropertyChanged("MapName");
            }
        }
        public string MapNick
        {
            get
            {
                return mapNick;
            }
            set
            {
                mapNick = value;
                commandSaveMap.RaiseCanExecuteChanged();
                NotifyPropertyChanged("MapNick");
            }
        }
        #endregion

        #region Builders
        public MapCreatorVM()
        {
            elementsSprite= new List<clsElementTypeSprite>();
            Elements = getAllElements();
            foreach (var e in Elements)
            {
                var elementSprite = new clsElementTypeSprite(e, convertirByteImagen(e));
                elementsSprite.Add(elementSprite);
            }
            FullMap = new List<clsElementMap>();
        }
        #endregion

        #region Metodos (a traducir)
        /// <summary>
        ///     <header> private List<clsElementType> getAllElements()</header>
        ///     <description>This method calls BL in order to take the list of elements in the database</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Returns the list of elements</postcondition>
        /// </summary>
        /// <returns>List<clsElementType> elements</returns>
        private List<clsElementType> getAllElements()
        {
            List<clsElementType> elementsList = new List<clsElementType>();

            try
            {
                elementsList = clsElementTypeQueryBL.getListOfElementTypeBL();
            }
            catch (Exception)
            {
                showError();
            }

            return elementsList;
        }
        /// <summary>
        ///     <header>public void imageTapped(object sender, TappedRoutedEventArgs e)</header>
        ///     <descripton>This method gets the axisX and axisY from the image tapped, and exchanges its source image for the selected item image</descripton>
        ///     <preconditions>None</preconditions>
        ///     <postconditions>The tapped source image is changed for the selected item image source</postconditions>
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">TappedRoutedEventArgs</param>
        public void imageTapped(object sender, TappedRoutedEventArgs e)
        {
            Image img = (Image) sender;
            double axisX = (double)img.GetValue(Canvas.LeftProperty);
            double axisY = (double)img.GetValue(Canvas.TopProperty);
            addElementMap(axisX, axisY, selectedElement.Id);
            img.Source = selectedElement.Imagen;
           
        }
        /// <summary>
        ///     <header>private void addElementMap(double axisX, double axisY)</header>
        ///     <description>This method add a new elementMap that represent a square in the map</description>
        ///     <preconditions>None</preconditions>
        ///     <postconditions>The elementMap is added to the list</postconditions>
        /// </summary>
        /// <param name="axisX">double</param>
        /// <param name="axisY">double</param>
        private void addElementMap(double axisX, double axisY, int id)
        {
            bool added = false;
            clsElementMap mapSquare = new clsElementMap(id, axisX, axisY);

            for (int i=0; i<FullMap.Count&&!added;i++)
            {
                if(FullMap[i].AxisX == axisX && FullMap[i].AxisY == axisY)
                {
                    FullMap[i] = mapSquare;
                    i = FullMap.Count;
                    added = true;
                }
            }
            if (!added)
            {
                FullMap.Add(mapSquare);
            }    
        }
        /// <summary>
        ///     <header>private async void SaveMapCommand_Execute()</header>
        ///     <description>This method calls BL to insert a list of elementMap</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Insert a list of element map</postcondition>
        /// </summary>
        private void SaveMapCommand_Execute()
        {
            try
            {
                visibility = Visibility.Visible;
                NotifyPropertyChanged("Visibility");
                int idMap = buildMap();
                foreach (var element in FullMap)
                {
                    clsElementMapManagerBL.postElementMapBL(idMap,element);
                }
                showSuccess();
            }
            catch
            {
                showError();
            }
            visibility = Visibility.Collapsed;
            NotifyPropertyChanged("Visibility");
        }
        /// <summary>
        ///     <header>private bool SaveMapCommand_CanExecute()</header>
        ///     <description>This method makes savecommand executable or not depends on the map name and the map nick</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Returns a bool that indicates if savecommand is executable</postcondition>
        /// </summary>
        /// <returns>bool executable</returns>
        private bool SaveMapCommand_CanExecute()
        {
            bool executable = false;
            if (!String.IsNullOrEmpty(mapName) && !String.IsNullOrEmpty(mapNick) && !mapNick.ToLower().Equals("default"))
            {
                executable = true;
            }

            return executable;
        }
        /// <summary>
        ///     <header>private int buildMap()</header>
        ///     <description>This method calls the Bl to insert a Map and gets the id</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Returns the id of the map inserted</postcondition>
        /// </summary>
        /// <returns>int idMap</returns>
        private int buildMap()
        {
            clsMap map = new clsMap(mapNick,mapName,size/50);
            int idMap = 0;
            try
            {
                idMap = clsMapManagerBL.procedureMapBL(map);
            }
            catch (Exception)
            {
                showError();
            }
            return idMap;
        }
        /// <summary>
        ///     <header>private ImageSource convertirByteImagen(clsElementType e)</header>
        ///     <description>This method converts an imagen into a byte array</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Returns a byte array</postcondition>
        /// </summary>
        private ImageSource convertirByteImagen(clsElementType e)
        {
            BitmapImage imagenBitMap = new BitmapImage();
            if (e != null && e.Sprite != null)
            {
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    var result = new BitmapImage();
                    stream.WriteAsync(e.Sprite.AsBuffer());
                    stream.Seek(0);
                    imagenBitMap.SetSource(stream);
                }
            }
            return imagenBitMap;
        }
        #endregion

        #region Commands
        /// <summary>
        ///     <header>private void LittleSizeCommand_Execute()</header>
        ///     <description>This command changes the size of the map to the minimun</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Changes the map's size</postcondition>
        /// </summary>
        private void LittleSizeCommand_Execute()
        {
            size = 800;
            NotifyPropertyChanged("Size");
        }
        /// <summary>
        ///     <header>private void MediumSizeCommand_Execute()</header>
        ///     <description>This command changes the size of the map to the medium</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Changes the map's size</postcondition>
        /// </summary>
        private void MediumSizeCommand_Execute()
        {
            size = 1200;
            NotifyPropertyChanged("Size");
        }
        /// <summary>
        ///     <header>private void BigSizeCommand_Execute()</header>
        ///     <description>This command changes the size of the map to the maximun</description>
        ///     <precondition>None</precondition>
        ///     <postcondition>Changes the map's size</postcondition>
        /// </summary>
        private void BigSizeCommand_Execute()
        {
            size = 1500;
            NotifyPropertyChanged("Size");
        }
        #endregion

    }
}
