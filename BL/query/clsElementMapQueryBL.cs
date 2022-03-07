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
        /// <summary>
        ///     <header>public static List<clsElementMap> getElementMapOfDefaultMap(int id)</header>
        ///     <description> This method calls DAL and returns a list of the elements of the map depending on which one you have selected with the id number received in parameters</description>
        ///     <precondition> the int id must be between 1-3 depending on which map u want to get elements from:
        ///     1.-Easy
        ///     2.-Medium
        ///     3.-Hard
        /// </precondition>
        ///     <postcondition> returns List<clsMap> defaultHardElementsMap to the UI </postcondition>
        /// </summary>
        /// <returns>returns List<clsElementMap> defaultHardElementsMap</returns>
        public static List<clsElementMap> getElementMapOfDefaultMap(int id)
        {
            return clsElementMapQueryDAL.getElementMapOfDefaultMap(id);
        }
    }
}
