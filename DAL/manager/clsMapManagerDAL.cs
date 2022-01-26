using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Entities;

namespace DAL.manager
{
    public class clsMapManagerDAL
    {
        /// <summary>
        ///     <header> public static int postMapDAL(clsMap oMap)</header>
        ///     <description> This method insert a new map in the database </description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the count of rows affected </postcondition>
        /// </summary>
        /// <param name="oMap">clsMap</param>
        /// <returns>int result</returns>
        public static int postMapDAL(clsMap oMap)
        {
            int result = 0;
            conecction.clsConnection myConnection = new conecction.clsConnection();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();

            try
            {
                connection = myConnection.getConnection();
                myCommand.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = oMap.Id;
                myCommand.Parameters.Add("@nick", System.Data.SqlDbType.VarChar).Value = oMap.Nick;
                myCommand.Parameters.Add("@size", System.Data.SqlDbType.Int).Value = oMap.Size;
                myCommand.Parameters.Add("@communityMap", System.Data.SqlDbType.Int).Value = oMap.CommunityMap;
                myCommand.CommandText = "INSERT INTO Map VALUES (@id,@nick,@size,@communityMap)";
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
