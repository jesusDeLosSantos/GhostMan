using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL.manager;
using System.Threading.Tasks;

namespace BL.manager
{
    public class clsElementMapManagerBL
    {
        /// <summary>
        ///     <header> public static int postElementMapBL(clsElementMap oElementMap)</header>
        ///     <description> This method insert a new elementMap in the database </description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the count of rows affected </postcondition>
        /// </summary>
        /// <param name="oElementMap">clsMap</param>
        /// <returns>int result</returns>
        public static int postElementMapBL(int idMap, clsElementMap oElementMap)
        {
            return clsElementMapManagerDAL.postElementMapDAL(idMap, oElementMap);
        }
    }
}
