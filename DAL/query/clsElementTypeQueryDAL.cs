using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Entities;

namespace DAL.query
{
    public class clsElementTypeQueryDAL
    {
        /// <summary>
        ///     <header>public static List<clsElementType> getListOfElementTypeDAL()</header>
        ///     <description> This method calls the database and returns a list of clsElementType</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsElementType> elementTypes to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsElementType> elementTypes</returns>
        public static List<clsElementType> getListOfElementTypeDAL()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsElementType> elementTypes = new List<clsElementType>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsElementType oElementType;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT * FROM GM_ElementTypes";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oElementType = new clsElementType();
                        oElementType.Id = (int)myReader["id"];
                        oElementType.Name = (String)myReader["name"];
                        oElementType.Category = (int)myReader["category"];
                        oElementType.Sprite = (byte[])myReader["sprite"];
                        elementTypes.Add(oElementType);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return elementTypes;
        }
        /// <summary>
        ///     <header>public static List<byte[]> getAllSpritesDAL()</header>
        ///     <description>This method calls the database and returns a list of byte[]</description>
        ///     <precondition>The connection with the database must works and you need to have Internet connection</precondition>
        ///     <postcondition>returns List<byte[]> allSprites to the BL</postcondition>
        /// </summary>
        /// <returns>returns List<byte[]> allSprites</returns>
        public static List<byte[]> getAllSpritesDAL()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsElementType> elementTypes = new List<clsElementType>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            List<byte[]> allSprites = new List<byte[]>();

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT sprite FROM GM_ElementTypes ORDER BY id";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        allSprites.Add((byte[])myReader["sprite"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return allSprites;

        }

        /// <summary>
        ///     <header>public static byte[] getSpriteOfElementTypeIdDAL(int elementTypeId)</header>
        ///     <description>This method calls the database and returns a byte array that contains the sprite of the given element type id</description>
        ///     <precondition>The element type id must exists in the database</precondition>
        ///     <postcondition>returns byte[] sprite to the BL</postcondition>
        /// </summary>
        /// <param name="elementTypeId">int</param>
        /// <returns>byte[] sprite</returns>
        public static byte[] getSpriteOfElementTypeIdDAL(int elementTypeId)
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsElementType> elementTypes = new List<clsElementType>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            byte[] sprite = null;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT * FROM GM_ElementTypes WHERE id = @elementId";
                myCommand.Parameters.Add("@elementId", System.Data.SqlDbType.Int).Value = elementTypeId;
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    myReader.Read(); //theres no need to do a while loop since there will be only 1 row in this reader
                    sprite = (byte[])myReader["sprite"];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return sprite;
        }
    }
}
