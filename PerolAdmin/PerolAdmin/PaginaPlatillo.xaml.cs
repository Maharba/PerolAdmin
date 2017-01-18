using System;
using System.Linq;
using Xamarin.Forms;
using PerolAdmin.Models;
using Acr.UserDialogs;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Plugin.Media;
using Imgur.API.Models;
using Syncfusion.SfCarousel.XForms;
using System.Collections.ObjectModel;

namespace PerolAdmin
{
    public partial class PaginaPlatillo : ContentPage
    {
        public string ID = "";
        private ObservableCollection<SfCarouselItem> _imagenes;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (ID != "")
            {
                //picker
                AzureDataServices ads = new AzureDataServices();
                var cate = await ads.ObtenerCategorias();


                Platillos platillo = await App.AzureService.ObtenerPlatillo(ID);
                int indice = 0;
                foreach (var a in cate)
                {
                    pickerCategoria.Items.Add(a.Nombre);
                    indice++;
                    if (a.Nombre == platillo.Categorias)
                    {
                        pickerCategoria.SelectedIndex = indice;
                    }
                }


                txtNombre.Text = platillo.Nombre;
                txtDescripcion.Text = platillo.Descripciom;
                txtPrecio.Text = platillo.Precio.ToString();
                imgFoto.Source = platillo.urlImagen;
                lblImgurFoto.Text = platillo.urlImagen;

                //pickerCategoria.Items[pickerCategoria.SelectedIndex]= platillo.Categorias;
                //pickerCategoria.SelectedIndex = platillo.;

            }
            else
            {
                AzureDataServices ads = new AzureDataServices();
                var cate = await ads.ObtenerCategorias();
                foreach (var a in cate)
                {
                    pickerCategoria.Items.Add(a.Nombre);
                }
            }
        }

        async void btnGuardar_Click(object sender, EventArgs a)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            string categoria = pickerCategoria.Items[pickerCategoria.SelectedIndex];
            string imageens = lblImgurFoto.Text;

            //if (
            //    txtNombre.Text.Contains(
            //        "'A' || 'B' || 'C' || 'D' || 'E' || 'F' ||'G' || 'H' ||'I' || 'J' || 'K' || 'L' || 'M' || 'N' || 'Ñ' || 'O' || 'P' || 'Q' || 'R' || 'S' || 'T' || 'U' || 'V' || 'W' || 'X' || 'Y' || 'Z' || 'a' || 'b' || 'c' || 'd' || 'e' || 'f' || 'g' || 'h' || 'i' || 'j' || 'k' || 'l' || 'm' || 'n' || 'ñ' || 'o' || 'p' || 'q' || 'r' || 's' || 't' || 'u' || 'v' || 'w' || 'x' || 'y' || 'z'"))
            //{

            //}
            //else
            //{
            //    await DisplayAlert("AVISO", "Solo letras", "Aceptar");
            //}






            if (ID == String.Empty)
            {
                var display = await DisplayAlert("AVISO", "¿Deseas Agregar el Platillo?", "Aceptar", "Cancelar");
                if (display == true)
                {
                    await App.AzureService.AgregarPlatillos(nombre, descripcion, precio, categoria, imageens);
                }
                //else
                //{
                //    await DisplayAlert("AVISO", "¡Se Cancelo el Agregar Platillo!", "Ok");

                //}

            }
            else
            {
                var display2 =
                    await DisplayAlert("AVISO", "¿Esta seguro de modificar el platillo?", "Aceptar", "Cancelar");
                if (display2 == true)
                {
                    await App.AzureService.ModificarPlatillo(ID, nombre, descripcion, precio, categoria, imageens);
                }

                //else
                //{
                //await DisplayAlert("AVISO", "¡Se Cancelo el Modificar Platillo!", "Ok");
                //}
            }
            await Navigation.PopAsync();

        }

        async void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {
                var display3 =
                    await DisplayAlert("AVISO", "¿Esta seguro de eliminar el platillo?", "Aceptar", "Cancelar");
                if (display3 == true)
                {
                    await App.AzureService.EliminarPlatillo(ID);
                }
                //else
                //{
                //    await DisplayAlert("AVISO", "¡Se Cancelo el Eliminar el Platillo!", "Ok");
                //}



                await Navigation.PopAsync();
            }
        }

        public PaginaPlatillo()
        {
            InitializeComponent();
            btnCargarFoto.Clicked += BtnCargarFoto_Clicked;

            
            _imagenes = new ObservableCollection<SfCarouselItem>();
            _imagenes.Add(new SfCarouselItem()
            {
                ItemContent = new Image()
                {
                    Source = "http://i.imgur.com/fRrAzCL.jpg"
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
    }
    }
