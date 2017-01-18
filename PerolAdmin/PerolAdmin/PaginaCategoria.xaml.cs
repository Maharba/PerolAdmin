using Acr.UserDialogs;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using PerolAdmin.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.SfCarousel.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class PaginaCategoria : ContentPage
    {
        private ObservableCollection<SfCarouselItem> _imagenes;
        public string ID = "";

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (ID != "")
            {
                var cargando = UserDialogs.Instance.Loading("Cargando...");
                Categorias categoria = await App.AzureService.ObtenerCategoria(ID);
                txtNombre.Text = categoria.Nombre;
                imgFoto.Source = categoria.urlImagen;
                cargando.Hide();
            }        
        }

        async void btnGuardar_Click(object sender, EventArgs a)
        {
            string nombre = txtNombre.Text;
            string urlimage = imgFoto.Source.ToString();
            if (ID == String.Empty)
                await App.AzureService.AgregarCategorias(nombre,urlimage);
            else
                await App.AzureService.ModificarCategoria(ID, nombre,urlimage);
            await Navigation.PopAsync();
        }

        async void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {
                await App.AzureService.EliminarCategoria(ID);
                await Navigation.PopAsync();
            }
        }

        public PaginaCategoria()
        {
            InitializeComponent();

            btnTomarFoto.Clicked += BtnTomarFotoOnClicked;
            btnTomarFoto2.Clicked += BtnTomarFoto2_Clicked;
            _imagenes = new ObservableCollection<SfCarouselItem>();
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

        private  async void BtnTomarFoto2_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                var foto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    Directory = "PruebasPerol",
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
                lblImgurFoto.Text = $"Ingur Link: {imagenImgur.Link}";
                var img = new Categorias();
                img.urlImagen = imagenImgur.Link;
                var caro = UserDialogs.Instance.Loading("Guardando..");
                await App.AzureService.AgregarCategorias(txtNombre.Text, img.urlImagen);
                caro.Hide();
                imgFoto.Source = imagenImgur.Link;
                lblImgurFoto.Text = imagenImgur.Link;
            


        }
        }

        private async void BtnTomarFotoOnClicked(object sender, EventArgs eventArgs)
        {
        
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsPickPhotoSupported)
                {
                    var foto = await CrossMedia.Current.PickPhotoAsync();
               
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
                 lblImgurFoto.Text = $"Ingur Link: {imagenImgur.Link}";
                    var img = new Categorias();
                    img.urlImagen = imagenImgur.Link;
                    var caro = UserDialogs.Instance.Loading("Guardando..");
                    await App.AzureService.AgregarCategorias(txtNombre.Text,img.urlImagen);
                    caro.Hide();
                    imgFoto.Source = imagenImgur.Link;
                lblImgurFoto.Text = imagenImgur.Link;
            }
        }
    
}
}
