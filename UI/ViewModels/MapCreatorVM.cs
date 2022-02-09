using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.query;
using Entities;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace UI.ViewModels
{
    public class MapCreatorVM
    {
        #region Attributes
        List<clsElementType> elements;
        clsMap emptyMap;
        List<clsElementMap> fullMap;
        clsElementType selectedElement;
        const int SQUARE_SIZE = 50;
        #endregion

        #region Getters y Setters
        public clsMap EmptyMap { get => emptyMap; set => emptyMap = value; }
        public List<clsElementType> Elements { get => elements; set => elements = value; }
        public List<clsElementMap> FullMap { get => fullMap; set => fullMap = value; }
        public clsElementType SelectedElement { get => selectedElement; set => selectedElement = value; }
        #endregion

        #region Builders
        public MapCreatorVM()
        {
            Elements = getAllElements();
            FullMap = new List<clsElementMap>();
        }
        #endregion

        #region Metodos (a traducir)
        private List<clsElementType> getAllElements()
        {
            List<clsElementType> elementsList = new List<clsElementType>();

            try {
                //elementsList= clsElementTypeQueryBL.getListOfElementTypeBL();
            }
            catch (Exception) 
            {
                //TODO pagina de error
            }

            return elementsList;
        }

        public void rectangleTapped(object sender, TappedRoutedEventArgs e)
        {
            Rectangle rec = (Rectangle) sender;
            double axisX = (double)rec.GetValue(Canvas.LeftProperty);
            double axisY = (double)rec.GetValue(Canvas.TopProperty);
            addElementMap(axisX, axisY);
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("/Assets/images/Horizontal_Wall_Test.png"));
            rec.Fill = imageBrush;
        }

        private void addElementMap(double axisX, double axisY)
        {
            bool added = false;
            clsElementMap mapSquare = new clsElementMap(1, axisX, axisY);

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

        #endregion
    }
}
