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
        int category;
        byte[] sprite;
        #endregion
        #region Builders
        public clsElementType()
        {

        }
        public clsElementType(int id, String name, String image, int category, byte[] sprite)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.sprite = sprite;
        }
        #endregion
        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Category { get => category; set => category = value; }
        public byte[] Sprite { get => sprite; set => sprite = value; }
        #endregion
    }
}
