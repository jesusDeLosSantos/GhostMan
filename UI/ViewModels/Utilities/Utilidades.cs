using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace UI.ViewModels.Utilities
{
    public class Utilidades
    {
        public static List<clsElementMap> listaParedes;
        public static bool canMove(int x, int y)
        {
            bool result = true;
            clsElementMap placeholder;
            for (int i = 0; i < listaParedes.Count && result; i++)
            {
                placeholder = listaParedes.ElementAt(i);
                if (placeholder.AxisX == x && placeholder.AxisY == y)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
