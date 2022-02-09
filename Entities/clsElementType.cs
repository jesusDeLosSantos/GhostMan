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
        String image;
        #endregion
        #region Builders
        public clsElementType()
        {

        }
        public clsElementType(int id,String name, String image)
        {
            this.id = id;
            this.name = name;
            this.image = image;
        }
        #endregion
        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public String Image { get => image; set => image = value; }
        #endregion
    }
}
