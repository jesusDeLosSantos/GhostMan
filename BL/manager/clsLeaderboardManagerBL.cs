using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL.manager;

namespace BL.manager
{
    public class clsLeaderboardManagerBL
    {
        /// <summary>
        ///     <header> public static int postLeaderboardBL(clsLeaderboard oLeaderboard)</header>
        ///     <description> This method insert a new leaderboard in the database </description>
        ///     <precondition> None </precondition>
        ///     <postcondition> Returns the count of rows affected </postcondition>
        /// </summary>
        /// <param name="oLeaderboard">clsLeaderboard</param>
        /// <returns>int result</returns>
        public static int postLeaderboardBL(clsLeaderboard oLeaderboard)
        {
            return clsLeaderboardManagerDAL.postLeaderboardDAL(oLeaderboard);
        }
    }
}
