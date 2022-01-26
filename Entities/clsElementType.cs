using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class clsElementType
    {
        #region Attributes
        int id;
        String name;
        #endregion
        #region Builders
        public clsElementType()
        {

        }
        public clsElementType(int id,String name)
        {
            this.id = id;
            this.name = name;
        }
        #endregion
        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        #endregion
    }
}
