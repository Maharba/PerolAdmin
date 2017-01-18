using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerolAdmin.Models;
using Xamarin.Forms;
using Acr.UserDialogs;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Syncfusion.SfCarousel.XForms;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Collections.ObjectModel;

namespace PerolAdmin
{
    public partial class PaginaPromociones : ContentPage
    {
        public string ID = "";
        private ObservableCollection<SfCarouselItem> _imagenes;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (ID != "")
            {
                Promociones promo = await App.AzureService.ObtenerPromocion(ID);
                txtNombre.Text = promo.Nombre;
                txtDescripcion.Text = promo.Descripcion;
                txtdia.Text = promo.Dia;
                imgFoto.Source = promo.urlImagen;
                lblImgurFoto.Text = promo.urlImagen;
            }
        }

        async void btnGuardar_Click(object sender, EventArgs a)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string dia = txtdia.Text;
            string urlimage = lblImgurFoto.Text;

            if (ID == String.Empty)
            {
                var display = await DisplayAlert("AVISO", "¿Deseas Agregar la Promoción", "Aceptar", "Cancelar");
                if (display == true)
                {
                    //if(txtNombre.Text == n)
                    await App.AzureService.AgregarPromocion(nombre,descripcion,dia, urlimage);
                }
                else
                {
                    await DisplayAlert("AVISO", "¡Se Cancelo el Agregar Promoción", "Ok");

                }

            }
            else
            {
                var display2 = await DisplayAlert("AVISO", "¿Esta seguro de modificar la Promoción?", "Aceptar", "Cancelar");
                if (display2 == true)
                {
                    await App.AzureService.ModificarPromocion(ID, nombre, descripcion,dia, urlimage);
                }

                else
                {
                    await DisplayAlert("AVISO", "¡Se Cancelo la Promoción!", "Ok");
                }
            }
            await Navigation.PopAsync();

        }


        async void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {
                var display3 = await DisplayAlert("AVISO", "¿Esta seguro de eliminar la Promoción", "Aceptar", "Cancelar");
                if (display3 == true)
                {
                    await App.AzureService.EliminarPromocion(ID);
                }
                else
                {
                    await DisplayAlert("AVISO", "¡Se Cancelo el Eliminar la Promoción!", "Ok");
                }



                await Navigation.PopAsync();
            }
        }
        public PaginaPromociones()
        {
            InitializeComponent();
            btnTomarFoto.Clicked += BtnTomarFoto_Clicked;
            //btnOtraGaler.Clicked += BtnOtraGaler_Clicked;
            //btnlogear.Clicked += Btnlogear_Clicked;
            btnCargarFoto.Clicked += BtnCargarFoto_Clicked;
            _imagenes = new ObservableCollection<SfCarouselItem>();
            _imagenes.Add(new SfCarouselItem()
            {
                ItemContent = new Image()
                {
                    Source = "http://i.imgur.com/fRrAzCL.jpg"
                }
            });
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
                        var caro = UserDialogs.Instance.Loading("Cargando..");
                        ImgurClient imgClient = new ImgurClient("cdf5157ba770e26",
                            "cf12ecb5fa699ddeec4712b6522e566884744dc5");
                        var endpoint = new ImageEndpoint(imgClient);
                        IImage image;
                        using (var stream = foto.GetStream())
                        {

                            image = await endpoint.UploadImageStreamAsync(stream);
                            _imagenes.Add(new SfCarouselItem()
                            {
                                ItemContent = new Image()
                                {
                                    Source = image.Link
                                }
                            });


                        }
                        //$"{image.Link}";
                        lblImgurFoto.Text = image.Link;
                        // var img = new Platillos();
                        // img.urlImagen = image.Link;

                        // await App.AzureService.AgregarPlatillos(txtNombre.Text, txtDescripcion.Text, decimal.Parse(txtPrecio.Text), pickerCategoria.Items.ToString(), imgFoto.Source.ToString());

                        imgFoto.Source = image.Link;
                        caro.Hide();
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
                        PhotoSize =  PhotoSize.Medium,
                        SaveToAlbum = true
                    });
                    try
                    {
                        ImgurClient imgClient = new ImgurClient("cdf5157ba770e26", "cf12ecb5fa699ddeec4712b6522e566884744dc5");
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
                        var img = new Promociones();
                        img.urlImagen = image.Link;
                        var caro = UserDialogs.Instance.Loading("Guardando..");
                        await App.AzureService.AgregarPromocion(txtNombre.Text, txtDescripcion.Text, txtdia.Text,img.urlImagen);
                        caro.Hide();
                        await DisplayAlert("AVISO", "Tu foto ha sido guardada en Promociones", "Ok");
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
