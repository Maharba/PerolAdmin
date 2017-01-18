using Syncfusion.SfCarousel.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfRotator.XForms;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace PerolAdmin
{
    public partial class RotatorPromos : ContentPage
    {
        private ObservableCollection<SfRotatorItem> _imagenes;

        public RotatorPromos()
        {
            InitializeComponent();
            _imagenes = new ObservableCollection<SfRotatorItem>();

            ObtenerFRottor();
            //carrusel.ItemsSource = _imagenes;
            //carrusel.ItemTemplate = new DataTemplate();

            _imagenes.Add(new SfRotatorItem()
            {
                ItemContent = new Image()
                {
                    Source = "http://i.imgur.com/fRrAzCL.jpg"
                }
            });
            try
            {
                var cargando = UserDialogs.Instance.Loading("Cargando...");

                // carousel.BindingContext = new ClassCarouselViewModel();
                rotator.ItemsSource = _imagenes;
                cargando.Hide();
               rotator.SelectedIndex = 0;
            }

            catch (System.Net.WebException ex)
            {
                DisplayAlert("Nse", "Se travo", "Aceptar", "Cancelar");
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                DisplayAlert("jaja", "otra vez se travo", "Aceptar", "Cancelar");
            }

            rotator.ItemsSource = _imagenes;
            rotator.ItemTemplate = new DataTemplate();
        }

        //Metodo para obtener las fotos en el carrousel//

        //public async void Task<IEnumerable<Galeriaa>> ObtenerFGaleria()
        public async void ObtenerFRottor()
        {
            var colecc = await App.AzureService.ObtenerPromociones();

            foreach (var a in colecc)
            {

                _imagenes.Add(new SfRotatorItem()
                {
                    ItemContent = new Image()
                    {
                        Source = a.urlImagen
                    }
                });
                rotator.ItemsSource = _imagenes;
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


    
}
}
