using Acr.UserDialogs;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.SfCarousel.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerolAdmin.Models;
using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class OtraGale : ContentPage
    {
        private ObservableCollection<SfCarouselItem> _imagenes;
        public OtraGale()
        {
            InitializeComponent(); btnTomarFoto.Clicked += BtnTomarFotoOnClicked;
            _imagenes = new ObservableCollection<SfCarouselItem>();

             ObtenerFGaleria();
            
           
            _imagenes.Add(new SfCarouselItem()
            {
                ItemContent = new Image()
                {
                    Source = "http://i.imgur.com/fRrAzCL.jpg"
                }
            });
            carrusel.ItemsSource = _imagenes;
            carrusel.ItemTemplate = new DataTemplate();
        }
        //Metodo para obtener las fotos en el carrousel//

            //public async void Task<IEnumerable<Galeriaa>> ObtenerFGaleria()
        public async void  ObtenerFGaleria()
        {
            var colecc = await App.AzureService.ObtenerFotosdeGaleria();

            foreach (var a in colecc)
            {

                _imagenes.Add(new SfCarouselItem()
                {
                    ItemContent = new Image()
                    {
                        Source = a.urlimagen
                    }
                });
                carrusel.ItemsSource = _imagenes;
                //return _imagenes;
                //carrusel.ItemsSource.Add(a.urlimagen);
            }

           


        }

        private async void BtnTomarFotoOnClicked(object sender, EventArgs eventArgs)
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                var foto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    Directory = "EjemploCarousel",
                    Name = "foto.jpg",
                    PhotoSize = PhotoSize.Medium,
                    SaveToAlbum = true
                });

                ImgurClient imgClient = new ImgurClient("cdf5157ba770e26", "cf12ecb5fa699ddeec4712b6522e566884744dc5");
                var endpoint = new ImageEndpoint(imgClient);
                IImage imagenImgur;
                using (var stream = foto.GetStream())
                {
                    var cargando = UserDialogs.Instance.Loading("Cargando a Imgur...");
                    imagenImgur = await endpoint.UploadImageStreamAsync(stream);
                    _imagenes.Add(new SfCarouselItem()
                    {
                        ItemContent = new Image()
                        {
                            Source = imagenImgur.Link
                        }
                    });
                    carrusel.ItemsSource = _imagenes;
                    cargando.Hide();
                }
                carrusel.SelectedIndex = 0;
                lblImgurFoto.Text = imagenImgur.Link;
            }
        }
    }
}
