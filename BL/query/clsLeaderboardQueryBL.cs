using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL.query;

namespace BL.query
{
    public class clsLeaderboardQueryBL
    {
        /// <summary>
        ///     <header>public static List<clsLeaderboard> getListOfLeaderboardBL()</header>
        ///     <description> This method calls DAL and returns a list of clsLeaderboard</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsLeaderboard> leaderboards to the UI </postcondition>
        /// </summary>
        /// <returns>returns List<clsLeaderboard> leaderboards</returns>
        public static List<clsLeaderboard> getListOfLeaderboardBL()
        {
            return clsLeaderBoardQueryDAL.getListOfLeaderboardDAL();
        }
    }
}
