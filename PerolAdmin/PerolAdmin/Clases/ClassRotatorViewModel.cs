using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerolAdmin.Clases
{
   public  class ClassRotatorViewModel
    {
        public ClassRotatorViewModel()
        {
            ImageCollection.Add(new ClassRotatorModel("s2en1.png"));
            ImageCollection.Add(new ClassRotatorModel("promo1.jpg"));
            ImageCollection.Add(new ClassRotatorModel("promo2.jpg"));
            ImageCollection.Add(new ClassRotatorModel("promo3.jpg"));
            ImageCollection.Add(new ClassRotatorModel("promo4.jpg"));

        }
        private List<ClassRotatorModel> imageCollection = new List<ClassRotatorModel>();

        public List<ClassRotatorModel> ImageCollection
        {
            get { return imageCollection; }
            set { imageCollection = value; }
        }

    }
}
