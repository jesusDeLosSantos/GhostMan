using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Entities;

namespace DAL.manager
{
    public class clsLeaderboardManagerDAL
    {
        /// <summary>
        ///     <header> public static int postLeaderboardDAL(clsLeaderboard oLeaderboard)</header>
        ///     <description> This method insert a new leaderboard in the database </description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the count of rows affected </postcondition>
        /// </summary>
        /// <param name="oLeaderboard">clsLeaderboard</param>
        /// <returns>int result</returns>
        public static int postLeaderboardDAL(clsLeaderboard oLeaderboard)
        {
            int result = 0;
            conecction.clsConnection myConnection = new conecction.clsConnection();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();

            try
            {
                connection = myConnection.getConnection();
                myCommand.Parameters.Add("@idMap", System.Data.SqlDbType.Int).Value = oLeaderboard.IdMap;
                myCommand.Parameters.Add("@nick", System.Data.SqlDbType.VarChar).Value = oLeaderboard.Nick;
                myCommand.Parameters.Add("@score", System.Data.SqlDbType.Int).Value = oLeaderboard.Score;
                myCommand.CommandText = "INSERT INTO Leaderboard VALUES (@idMap,@nick,@score)";
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
