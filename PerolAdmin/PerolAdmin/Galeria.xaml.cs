using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using System.Net;
using Acr.UserDialogs;
using Imgur.API.Enums;
using PerolAdmin.Models;
using Syncfusion.SfCarousel.XForms;
using System.Collections.ObjectModel;

namespace PerolAdmin
{
    public partial class Galeria : ContentPage
    {

        private ObservableCollection<SfCarouselItem> _imagenes;

        public Galeria()
        {
        InitializeComponent();
            btnTomarFoto.Clicked += BtnTomarFoto_Clicked;
            btnCargarFoto.Clicked += BtnCargarFoto_Clicked;
            //btnOtraGaler.Clicked += BtnOtraGaler_Clicked;
            btnlogear.Clicked += Btnlogear_Clicked;
            _imagenes = new ObservableCollection<SfCarouselItem>();
            _imagenes.Add(new SfCarouselItem()
            {
                ItemContent = new Image()
                {
                    Source = "http://i.imgur.com/NrhOBnn.jpg"
                }
            });
            //Car.ItemsSource = _imagenes;
            //carrusel.ItemTemplate = new DataTemplate();
        }

        private async void BtnCargarFoto_Clicked(object sender, EventArgs e)
        {

            try
            {
                // await CrossMedia.Current.Initialize();
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsPickPhotoSupported)
                {
                    var foto = await CrossMedia.Current.PickPhotoAsync();
                    try
                    {
                        ImgurClient imgClient = new ImgurClient("cdf5157ba770e26",
                            "cf12ecb5fa699ddeec4712b6522e566884744dc5");
                        var endpoint = new ImageEndpoint(imgClient);
                        IImage image;
                        using (var stream = foto.GetStream())
                        {
                            var cargando = UserDialogs.Instance.Loading("Cargando...");
                            image = await endpoint.UploadImageStreamAsync(stream);
                            _imagenes.Add(new SfCarouselItem()
                            {
                                ItemContent = new Image()
                                {
                                    Source = image.Link
                                }
                            });

                            cargando.Hide();
                        }

                        lblImgurFoto.Text = $"Ingur Link: {image.Link}";
                        var img = new Models.Galeria();
                        img.urlimagen= image.Link;
                        var caro = UserDialogs.Instance.Loading("Guardando..");
                        await App.AzureService.GuardarUrlGaleria(img.urlimagen);
                        caro.Hide();
                        var caros = UserDialogs.Instance.Alert("Imagen Guardada", null, "Aceptar");
                        imgFoto.Source = image.Link;
                       
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                UserDialogs.Instance.ShowError("ERROR", 2000);
                //await DisplayAlert("Nse", "Se travo", "Aceptar", "Cancelar");
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                UserDialogs.Instance.ShowError("ERROR", 2000);
                //await  DisplayAlert("jaja", "otra vez se travo", "Aceptar", "Cancelar");
            }

        
    }

        private async void BtnOtraGaler_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OtraGale());
        }

        private async void Btnlogear_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GaleriaCarousel());
        }

        private async void BtnTomarFoto_Clicked(object sender, EventArgs e)
        {

            try
            {
                // await CrossMedia.Current.Initialize();
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    var foto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                    {

                        Name = "foto.png",
                        Directory = "PruebasPerol",
                        PhotoSize =  PhotoSize.Small,
                        SaveToAlbum = true
                    });
                    try
                    {
                        ImgurClient imgClient = new ImgurClient("cdf5157ba770e26",
                            "cf12ecb5fa699ddeec4712b6522e566884744dc5");
                        var endpoint = new ImageEndpoint(imgClient);
                        IImage image;
                        using (var stream = foto.GetStream())
                        {
                            var cargando = UserDialogs.Instance.Loading("Cargando...");
                            image = await endpoint.UploadImageStreamAsync(stream);
                            _imagenes.Add(new SfCarouselItem()
                            {
                                ItemContent = new Image()
                                {
                                    Source = image.Link
                                }
                            });

                            cargando.Hide();
                        }

                        lblImgurFoto.Text = $"Ingur Link: {image.Link}";
                        var img = new Models.Galeria();
                        img.urlimagen = image.Link;
                        var caro = UserDialogs.Instance.Loading("Guardando..");
                        await App.AzureService.GuardarUrlGaleria(img.urlimagen);
                        caro.Hide();
                        imgFoto.Source = image.Link;

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                UserDialogs.Instance.ShowError("ERROR", 2000);
                //await DisplayAlert("Nse", "Se travo", "Aceptar", "Cancelar");
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                UserDialogs.Instance.ShowError("ERROR", 2000);
                //await  DisplayAlert("jaja", "otra vez se travo", "Aceptar", "Cancelar");
            }

        }
    }
  

}