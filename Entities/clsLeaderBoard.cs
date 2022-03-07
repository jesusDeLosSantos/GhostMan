using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class clsLeaderboard
    {
        #region Attributes
        int idMap;
        String nick;
        string score;
        #endregion
        #region Builders
        public clsLeaderboard()
        {

        }
        public clsLeaderboard(int idMap,String nick,string score)
        {
            this.idMap = idMap;
            this.nick = nick;
            this.score = score;
        }
        #endregion
        #region Getters & Setters
        public int IdMap { get => idMap; set => idMap = value; }
        public string Nick { get => nick; set => nick = value; }
        public string Score { get => score; set => score = value; }
        #endregion
    }
}
