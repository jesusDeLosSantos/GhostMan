using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Entities;

namespace DAL.manager
{
    public class clsElementMapManagerDAL
    {
        /// <summary>
        ///     <header> public static int postElementMapDAL(clsElementMap oElementMap)</header>
        ///     <description> This method insert a new elementMap in the database </description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the count of rows affected </postcondition>
        /// </summary>
        /// <param name="oElementMap">clsMap</param>
        /// <returns>int result</returns>
        public static int postElementMapDAL(clsElementMap oElementMap)
        {
            int result = 0;
            conecction.clsConnection myConnection = new conecction.clsConnection();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();

            try
            {
                connection = myConnection.getConnection();
                myCommand.Parameters.Add("@idMap", System.Data.SqlDbType.Int).Value = oElementMap.IdMap;
                myCommand.Parameters.Add("@idElement", System.Data.SqlDbType.Int).Value = oElementMap.IdElement;
                myCommand.Parameters.Add("@axisX", System.Data.SqlDbType.Int).Value = oElementMap.AxisX;
                myCommand.Parameters.Add("@asixY", System.Data.SqlDbType.Int).Value = oElementMap.AxisY;
                myCommand.CommandText = "INSERT INTO ElementMap VALUES (@idMap,@idElement,@axisX,@axisY)";
                myCommand.Connection = connection;
                result = myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConnection.closeConnection(ref connection);
            }
            return result;
        }
    }
}
