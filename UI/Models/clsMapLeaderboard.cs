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
        List<clsElementMap> elementList;
        #endregion
        #region Builders
        public clsMapLeaderboard() : base()
        {

        }

        public clsMapLeaderboard(clsMap map, List<clsLeaderboardWithPosition> leaderboards, List<clsElementMap> elementList) : base(map.Id, map.Nick, map.Size, map.CommunityMap)
        {
            this.leaderboards = leaderboards;
            this.elementList = elementList;
        }
        #endregion
        #region Getters & Setters
        public List<clsLeaderboardWithPosition> Leaderboards { get => leaderboards; set => leaderboards = value; }
        public List<clsElementMap> ElementList { get => elementList; set => elementList = value; }
        #endregion
    }
}
