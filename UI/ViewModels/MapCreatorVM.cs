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
        List<clsElementType> elements;
        List<clsElementTypeSprite> elementsSprite;
        clsMap emptyMap;
        List<clsElementMap> fullMap;
        clsElementTypeSprite selectedElement = new clsElementTypeSprite();
        const int SQUARE_SIZE = 50;
        DelegateCommand commandSaveMap;
        ImageSource spriteSelected;
        #endregion

        #region Getters y Setters
        public clsMap EmptyMap { get => emptyMap; set => emptyMap = value; }
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
        private List<clsElementType> getAllElements()
        {
            List<clsElementType> elementsList = new List<clsElementType>();

            try {
                elementsList= clsElementTypeQueryBL.getListOfElementTypeBL();
            }
            catch (Exception) 
            {
                ContentDialog error = new ContentDialog()       //TODO vista error
                {
                    Content = "Ha ocurrido un error.",
                    CloseButtonText = "Ok"
                };

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

            for (int i=0; i<FullMap.Count;i++)
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
 
        private async void SaveMapCommand_Execute()
        {
            try
            {

                clsMapManagerBL.postMapBL(EmptyMap);
                foreach (var map in FullMap)
                {
                    clsElementMapManagerBL.postElementMapBL(map);
                }

                ContentDialog guardar = new ContentDialog()
                {
                    Content = "Se han guardado los cambios.",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult respuesta = await guardar.ShowAsync();
            }
            catch
            {
                ContentDialog error = new ContentDialog()
                {
                    Content = "Ha ocurrido un error.",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult resultado = await error.ShowAsync();
            }

        }
        private bool SaveMapCommand_CanExecute()
        {
            return true;
        }


        /// <summary>
        /// Este metodo se encarga de convertir una array de bytes a imagen
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
    }
}
