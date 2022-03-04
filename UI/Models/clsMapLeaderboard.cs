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
        List<clsLeaderboardWithPosition> leaderboards;
        #endregion
        #region Builders
        public clsMapLeaderboard() : base()
        {

        }

        public clsMapLeaderboard(clsMap map, List<clsLeaderboardWithPosition> leaderboards) : base(map.Id, map.Nick, map.Name, map.Size, map.CommunityMap)
        {
            this.leaderboards = leaderboards;
        }
        #endregion
        #region Getters & Setters
        public List<clsLeaderboardWithPosition> Leaderboards { get => leaderboards; set => leaderboards = value; }
        #endregion
    }
}
