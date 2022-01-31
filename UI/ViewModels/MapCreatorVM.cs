using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.query;
using Entities;

namespace UI.ViewModels
{
    public class MapCreatorVM
    {
        List<clsElementType> elements;
        clsMap emptyMap;
        clsElementMap fullMap;

        public clsMap EmptyMap { get => emptyMap; set => emptyMap = value; }
        public List<clsElementType> Elements { get => elements; set => elements = value; }
        public clsElementMap FullMap { get => fullMap; set => fullMap = value; }

        public MapCreatorVM()
        { 
            //Elements = getAllElements();
        }

        private List<clsElementType> getAllElements()
        {
            List<clsElementType> elementsList = new List<clsElementType>();

            try {
                elementsList= clsElementTypeQueryBL.getListOfElementTypeBL();
            }
            catch (Exception) 
            {
                //TODO pagina de error
            }

            return elementsList;
        }
    }
}
