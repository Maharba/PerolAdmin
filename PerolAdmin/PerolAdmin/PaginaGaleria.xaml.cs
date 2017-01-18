using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerolAdmin.Models;
using Xamarin.Forms;
using Image = Imgur.API.Models.Impl.Image;

namespace PerolAdmin
{
    public partial class PaginaGaleria : ContentPage
    {
        public string ID = "";
        public PaginaGaleria()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var cargando = UserDialogs.Instance.Loading("Cargando...");
            //slblImage.Source= await App.AzureService.ObtenerunaFotodeGaleria(ID));


            cargando.Hide();
        }

        async void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {
                var display3 = await DisplayAlert("AVISO", "¿Esta seguro de eliminar la foto", "Aceptar", "Cancelar");
                if (display3 == true)
                {
                    await App.AzureService.EliminarunaFotodeGaleria(ID);
                }

                await Navigation.PopAsync();
            }
        }
    }
}
