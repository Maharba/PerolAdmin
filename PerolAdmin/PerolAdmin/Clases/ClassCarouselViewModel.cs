using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerolAdmin.Clases;
using PerolAdmin.Models;

namespace PerolAdmin
{
    public class ClassCarouselViewModel
    {
        AzureDataServices aazure = new AzureDataServices();

        public ClassCarouselViewModel()
        {
            //ImageCollection.Add(new ClassCarouselModel("P0.png"));
           obtenerListaFotosGaleria();
        } 
        public async void obtenerListaFotosGaleria()
        {
            
            var url = await aazure.ObtenerFotosdeGaleria();

            foreach (var a in url)
            {
                ImageCollection.Add(new ClassCarouselModel(a.urlimagen));
            }
        }

        private List<ClassCarouselModel> imageCollection = new List<ClassCarouselModel>();

        public List<ClassCarouselModel> ImageCollection
        {
            get { return imageCollection; }
            set { imageCollection = value; }
        }
    }
}
