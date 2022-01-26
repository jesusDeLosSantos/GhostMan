using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL.manager;

namespace BL.manager
{
    public class clsMapManagerBL
    {
        /// <summary>
        ///     <header> public static int postMapBL(clsMap oMap)</header>
        ///     <description> This method insert a new map in the database </description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the count of rows affected </postcondition>
        /// </summary>
        /// <param name="oMap">clsMap</param>
        /// <returns>int result</returns>
        public static int postMapBL(clsMap oMap)
        {
            return clsMapManagerDAL.postMapDAL(oMap);
        }
    }
}
