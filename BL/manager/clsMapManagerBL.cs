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
        ///     <header> public static int procedureMapBL(clsMap oMap)</header>
        ///     <description>This method execute a procedure which inserts a new map and returns his id</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the id of the inserted map</postcondition>
        /// </summary>
        /// <param name="oMap">clsMap</param>
        /// <returns>int idMap</returns>
        public static int procedureMapBL(clsMap oMap)
        {
            return clsMapManagerDAL.procedureMapDAL(oMap);
        }
    }
}
