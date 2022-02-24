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
        ///     <description> This method execute a procedure which inserts a new map and returns his id</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the id of the inserted map</postcondition>
        /// </summary>
        /// <param name="oMap">clsMap</param>
        /// <returns>int idMap</returns>
        public static int procedureMapDAL(clsMap oMap)
        {
            int result = 0;
            conecction.clsConnection myConnection = new conecction.clsConnection();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                connection = myConnection.getConnection();
                myCommand.Parameters.Add("@nick", System.Data.SqlDbType.VarChar).Value = oMap.Nick;
                myCommand.Parameters.Add("@mapName", System.Data.SqlDbType.VarChar).Value = oMap.Name;
                myCommand.Parameters.Add("@size", System.Data.SqlDbType.Int).Value = oMap.Size;
                myCommand.Parameters.Add("@communityMap", System.Data.SqlDbType.Int).Value = oMap.CommunityMap;
                var returnParameter = myCommand.Parameters.Add("@mapId", System.Data.SqlDbType.Int);
                returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;
                myCommand.CommandText = "insertMap";
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();
                result = (int)returnParameter.Value;
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
