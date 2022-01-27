using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace UI.Models
{
    public class clsMapLeaderboard : clsMap
    {
        #region Attributes
        clsLeaderboard leaderboard;
        #endregion
        #region Builders
        public clsMapLeaderboard() : base()
        {

        }

        public clsMapLeaderboard(clsMap map, clsLeaderboard leaderboard) : base(map.Id, map.Nick, map.Size, map.CommunityMap)
        {
            this.leaderboard = leaderboard;
        }
        #endregion
        #region Getters & Setters
        public clsLeaderboard Leaderboard { get => leaderboard; set => leaderboard = value; }
        #endregion
    }
}
