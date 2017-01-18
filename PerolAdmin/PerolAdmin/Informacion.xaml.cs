using PerolAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class Informacion : ContentPage
    {
       
        public Informacion()
        {
            InitializeComponent();

          
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var cargando = UserDialogs.Instance.Loading("Cargando...");
            InfoPerol histo = await App.AzureService.ObtenerInfoPerol();
            lblHistoria.Text = histo.Historia;
            lblDireccion.Text = histo.Direccion;
            lblHorario.Text = histo.Horario;
            lblTelefono.Text = histo.Telefono;
            cargando.Hide();

        }
        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var cargand = UserDialogs.Instance.Loading("Cargando...");
        //    var perol = await App.AzureService.ObtenerInfoPeroool();
            
        //    lblHistoria.Text= perol.
        //    cargand.Hide();

        //}

        //private void lsvInformacion_Selected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem != null)
        //    {

        //        InfoPerol infoPerol= e.SelectedItem as InfoPerol;
        //        EditarInformacion pagina = new EditarInformacion();
        //        pagina.ID = infoPerol.Id;
        //        Navigation.PushAsync(pagina);

        //    }
        //}
        private void btnNuevo_Click(object sender, EventArgs a)
        {
            Navigation.PushAsync(new EditarInformacion());
        }





        private void btnEditar_Click(object sender, EventArgs a)
        {
            
            Navigation.PushAsync(new EditarInformacion());
        }
    }
}
