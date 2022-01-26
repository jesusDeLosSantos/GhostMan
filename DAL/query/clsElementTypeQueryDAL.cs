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
                myCommand.CommandText = "SELECT * FROM ElementType";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oElementType = new clsElementType();
                        oElementType.Id = (int)myReader["id"];
                        oElementType.Name = (String)myReader["name"];
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
    }
}
