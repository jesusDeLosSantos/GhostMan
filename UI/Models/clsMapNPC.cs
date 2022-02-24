using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.utilities;

namespace UI.Models
{
    public class clsMapNPC : clsVMBase
    {
        clsMap map;

        public clsMap Map
        {
            get { return map; }
            set
            {
                map = value;
                NotifyPropertyChanged("Map");
            }
        }

        public clsMapNPC()
        {
            Map = new clsMap();
        }
    }
}
