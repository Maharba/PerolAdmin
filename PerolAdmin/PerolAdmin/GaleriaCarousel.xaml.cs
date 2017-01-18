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
    public partial class GaleriaCarousel : ContentPage
    {
        private ObservableCollection<SfCarouselItem> _imagenes;

        public GaleriaCarousel()
        {
            InitializeComponent();
            //_imagenes = new ObservableCollection<SfCarouselItem>();

            //ObtenerFGaleria();
            ////carrusel.ItemsSource = _imagenes;
            ////carrusel.ItemTemplate = new DataTemplate();

            //_imagenes.Add(new SfCarouselItem()
            //{
            //    ItemContent = new Image()
            //    {
            //        Source = "http://i.imgur.com/fRrAzCL.jpg"
            //    }
            //});
            //try
            //{
            //    var cargando = UserDialogs.Instance.Loading("Cargando...");

            //    // carousel.BindingContext = new ClassCarouselViewModel();
            //    carrusel.ItemsSource = _imagenes;
            //    cargando.Hide();
            //    carrusel.SelectedIndex = 0;
            //}

            //catch (System.Net.WebException ex)
            //{
            //    DisplayAlert("Nse", "Se travo", "Aceptar", "Cancelar");
            //}
            //catch (System.Threading.Tasks.TaskCanceledException)
            //{
            //    DisplayAlert("jaja", "otra vez se travo", "Aceptar", "Cancelar");
            //}

            //carrusel.ItemsSource = _imagenes;
            //carrusel.ItemTemplate = new DataTemplate();
        }
        public string ID = "";


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var cargando = UserDialogs.Instance.Loading("Cargando...");
          lsvGaleria.ItemsSource = await App.AzureService.ObtenerFotosdeGaleria();

        
            cargando.Hide();
        }

        //Metodo para obtener las fotos en el carrousel//

        //public async void Task<IEnumerable<Galeriaa>> ObtenerFGaleria()
        public async void ObtenerFGaleria()
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
               // carrusel.ItemsSource = _imagenes;
                //return _imagenes;
                //carrusel.ItemsSource.Add(a.urlimagen);
            }

        }

        //    InitializeComponent();
        //    btnTomarFoto.Clicked += BtnTomarFotoOnClicked;
        //    _imagenes = new ObservableCollection<SfCarouselItem>();
        //    _imagenes.Add(new SfCarouselItem()
        //    {
        //        ItemContent = new Image()
        //        {
        //            Source = "http://i.imgur.com/fRrAzCL.jpg"
        //        }
        //    });
        //    Carousel.ItemsSource = _imagenes;
        //    Carousel.ItemTemplate = new DataTemplate();
        //}

        //private async void BtnTomarFotoOnClicked(object sender, EventArgs eventArgs)
        //{
        //    if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        var foto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
        //        {
        //            Directory = "EjemploCarousel",
        //            Name = "foto.jpg",
        //           // PhotoSize = PhotoSize.Medium,
        //            SaveToAlbum = true
        //        });

        //        ImgurClient imgClient = new ImgurClient("cdf5157ba770e26", "cf12ecb5fa699ddeec4712b6522e566884744dc5");
        //        var endpoint = new ImageEndpoint(imgClient);
        //        IImage imagenImgur;
        //        using (var stream = foto.GetStream())
        //        {
        //            var cargando = UserDialogs.Instance.Loading("Cargando a Imgur...");
        //            imagenImgur = await endpoint.UploadImageStreamAsync(stream);
        //            _imagenes.Add(new SfCarouselItem()
        //            {
        //                ItemContent = new Image()
        //                {
        //                    Source = imagenImgur.Link
        //                }
        //            });
        //            Carousel.ItemsSource = _imagenes;
        //            cargando.Hide();
        //        }
        //        Carousel.SelectedIndex = 0;
        //        lblImgurFoto.Text = imagenImgur.Link;
        //    }


        private  void LsvGaleria_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Models.Galeria gal = e.SelectedItem as Models.Galeria;
                PaginaGaleria pagina = new PaginaGaleria();
                pagina.ID = gal.Id;
                Navigation.PushAsync(pagina);

            }

        }
    }
}
