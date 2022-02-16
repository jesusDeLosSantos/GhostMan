using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UI.Models
{
    public class clsElementTypeSprite : clsElementType
    {
        ImageSource imagen;

        public clsElementTypeSprite(clsElementType e, ImageSource image)
        {
            this.Imagen = image;
            this.Sprite = e.Sprite;
            this.Id = e.Id;
            this.Name = e.Name;
            this.Category = e.Category;
        }

        public clsElementTypeSprite() { }

        public ImageSource Imagen { get => imagen; set => imagen = value; }
    }
}
