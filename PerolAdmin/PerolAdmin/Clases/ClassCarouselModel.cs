using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerolAdmin.Clases
{
    public class ClassCarouselModel
    {
        
            public ClassCarouselModel(string imagestr)
            {
                Image = imagestr;
            }
            private string _image;

            public string Image
            {
                get { return _image; }
                set { _image = value; }
            }
        }
}
