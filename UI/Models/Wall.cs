using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    public class Wall
    {
        public Wall(int x, int y)
        {
            xAxis = x;
            yAxis = y;
        }
        public int xAxis { get; set; }
        public int yAxis { get; set; }
    }
}