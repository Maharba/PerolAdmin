using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerolAdmin.Clases
{
    public class ClassRotatorModel
    {
        public ClassRotatorModel(string imagestr)
        {
            Image = imagestr;
        }
        private String _image;
        public String Image
        {
            get { return _image; }
            set { _image = value; }
        }
        }
    
}
