using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class clsElementMap
    {
        #region Attributes
        int idMap;
        int idElement;
        double axisX;
        double axisY;
        #endregion
        #region Builders
        public clsElementMap()
        {

        }

        public clsElementMap(int idMap, int idElement, double axisX, double axisY)
        {
            this.idMap = idMap;
            this.idElement = idElement;
            this.axisX = axisX;
            this.axisY = axisY;
        }
        public clsElementMap(int idElement, double axisX, double axisY)
        {
            this.idMap = 0;
            this.idElement = idElement;
            this.axisX = axisX;
            this.axisY = axisY;
        }
        #endregion
        #region Getters & Setters
        public int IdMap { get => idMap; set => idMap = value; }
        public int IdElement { get => idElement; set => idElement = value; }
        public double AxisX { get => axisX; set => axisX = value; }
        public double AxisY { get => axisY; set => axisY = value; }
        #endregion
    }
}
