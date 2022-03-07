using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL.query;

namespace BL.query
{
    public class clsElementTypeQueryBL
    {
        /// <summary>
        ///     <header>public static List<clsElementType> getListOfElementTypeDAL()</header>
        ///     <description> This method calls DAL and returns a list of clsElementType</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsElementType> elementTypes to the UI </postcondition>
        /// </summary>
        /// <returns>returns List<clsElementType> elementTypes</returns>
        public static List<clsElementType> getListOfElementTypeBL()
        {
            return clsElementTypeQueryDAL.getListOfElementTypeDAL();
        }
        /// <summary>
        ///     <header>public static List<byte[]> getAllSpritesBL()</header>
        ///     <description>This method calls DAL and returns a List of byte[] that contains all the sprites in the database</description>
        ///     <precondition>The connection must works and you need to have Internet connection</precondition>
        ///     <postcondition>returns List<byte[]> allSprites to the UI </postcondition>
        /// </summary>
        /// <returns>List<byte[]> allSprites </returns>
        public static List<byte[]> getAllSpritesBL()
        {
            return clsElementTypeQueryDAL.getAllSpritesDAL();
        }
    }
}
