using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL.query;

namespace BL.query
{
    public class clsMapQueryBL
    {
        /// <summary>
        ///     <header>public static List<clsLeaderboard> getListOfMapBL()</header>
        ///     <description> This method calls DAL and returns a list of clsMap</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsMap> maps to the UI </postcondition>
        /// </summary>
        /// <returns>returns List<clsMap> maps</returns>
        public static List<clsMap> getListOfMapsBL()
        {
            return clsMapQueryDAL.getListOfMapsDAL();
        }
        /// <summary>
        ///     <header>public static List<clsMap> getListOfCustomMapsBL()</header>
        ///     <description> This method calls DAL and returns a list of custom maps</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsMap> maps to the UI </postcondition>
        /// </summary>
        /// <returns>returns List<clsMap> maps</returns>
        public static List<clsMap> getListOfCustomMapsBL()
        {
            return clsMapQueryDAL.getListOfCustomMapsDAL();
        }
        /// <summary>
        ///     <header>public static List<clsMap> getListOfDefaultMapsBL()</header>
        ///     <description> This method calls DAL and returns a list of default maps</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsMap> maps to the UI </postcondition>
        /// </summary>
        /// <returns>returns List<clsMap> maps</returns>
        public static List<clsMap> getListOfDefaultMapsBL()
        {
            return clsMapQueryDAL.getListOfDefaultMapsDAL();
        }
    }
}
