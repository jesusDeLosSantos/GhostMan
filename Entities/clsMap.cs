using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class clsMap
    {
        #region Attributes
        int id;
        String nick;
        int size;
        int communityMap;
        #endregion
        #region Builders

        public clsMap()
        {

        }

        public clsMap(int id,String nick,int size,int communityMap)
        {
            this.id = id;
            this.nick = nick;
            this.size = size;
            this.communityMap = communityMap;
        }
        #endregion
        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Nick { get => nick; set => nick = value; }
        public int Size { get => size; set => size = value; }
        public int CommunityMap { get => communityMap; set => communityMap = value; }
        #endregion
    }
}
