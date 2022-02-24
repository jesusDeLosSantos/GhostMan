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
        ///     <header>public static byte[] getSpriteOfElementTypeIdBL(int elementTypeId)</header>
        ///     <description>This method calls DAL and returns a byte array that contains the sprite of the given element type id</description>
        ///     <precondition>The element type id must exists in the database</precondition>
        ///     <postcondition>returns byte[] sprite to the UI</postcondition>
        /// </summary>
        /// <param name="elementTypeId">int</param>
        /// <returns>byte[] sprite</returns>
        public static byte[] getSpriteOfElementTypeIdBL(int elementTypeId)
        {
            return clsElementTypeQueryDAL.getSpriteOfElementTypeIdDAL(elementTypeId);
        }
    }
}
