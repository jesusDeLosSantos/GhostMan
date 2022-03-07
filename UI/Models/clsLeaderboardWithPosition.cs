using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace UI.Models
{
    public class clsLeaderboardWithPosition : clsLeaderboard
    {
        int position;

        public clsLeaderboardWithPosition() : base()
        {

        }
        public clsLeaderboardWithPosition(clsLeaderboard leaderboard, int position) : base(leaderboard.IdMap, leaderboard.Nick, leaderboard.Score)
        {
            this.position = position;
        }

        public int Position
        {
            get => position;
            set => position = value;
        }
    }
}
