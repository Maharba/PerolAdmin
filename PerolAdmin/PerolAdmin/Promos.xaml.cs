using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using PerolAdmin.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.SfCarousel.XForms;
using Syncfusion.SfRotator.XForms;
using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class Promos : ContentPage
    {
        private ObservableCollection<SfRotatorItem> _imagenes;

        public Promos()
        {
            InitializeComponent();
            btnTomarFoto.Clicked += BtnTomarFoto_Clicked;
            btneditarpromo.Clicked += Btneditarpromo_Clicked;

            btnlogear.Clicked += Btnlogear_Clicked;
            _imagenes = new ObservableCollection<SfRotatorItem>();
            _imagenes.Add(new SfRotatorItem()
            {
                ItemContent = new Image()
                {
                    Source = "http://i.imgur.com/fRrAzCL.jpg"
                }
            });
            //Car.ItemsSource = _imagenes;
          //  rotator.ItemTemplate = new DataTemplate();
        }

        private async void Btneditarpromo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarPromociones());
        }

        private async void Btnlogear_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RotatorPromos());
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
                        //PhotoSize =  PhotoSize.Medium,
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
                            _imagenes.Add(new SfRotatorItem()
                            {
                                ItemContent = new Image()
                                {
                                    Source = image.Link
                                }
                            });

                            cargando.Hide();
                        }

                        lblImgurFoto.Text = $"Ingur Link: {image.Link}";
                        var img = new Promociones();
                        img.urlImagen = image.Link;
                        var caro = UserDialogs.Instance.Loading("Guardando..");
                        await App.AzureService.AgregarPromocion(img.Nombre, img.Descripcion, img.Dia, img.urlImagen);
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
