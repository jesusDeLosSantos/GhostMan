using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL.query;

namespace BL.query
{
    public class clsElementMapQueryBL
    {
        /// <summary>
        ///     <header> public static List<clsElementMap> getListOfElementMapBL()</header>
        ///     <description> This method calls DAL and returns a list of clsElementMap</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsElementMap> elementsMap to the UI </postcondition>
        /// </summary>
        /// <returns>returns List<clsElementMap> elementsMap</returns>
        public static List<clsElementMap> getListOfElementMapBL()
        {
            return clsElementMapQueryDAL.getListOfElementMapDAL();
        }
    }
}
