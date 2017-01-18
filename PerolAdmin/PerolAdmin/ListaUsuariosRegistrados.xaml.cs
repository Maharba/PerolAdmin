using PerolAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class ListaUsuariosRegistrados : ContentPage
    {
        public ListaUsuariosRegistrados()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            lblActivityIndicator.IsRunning = true;
            lsvUsuariosRegistrados.ItemsSource = await App.AzureService.ObtenerUsuarios();
            lblActivityIndicator.IsRunning = false;

        }

        private void lsvUsuariosRegistrados_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                Usuarios usu= e.SelectedItem as Usuarios;
                PaginaUsuariosRegistrados pagina = new PaginaUsuariosRegistrados();
                pagina.ID = usu.Id;
                Navigation.PushAsync(pagina);

            }
        }

        private void btnNuevo_Click(object sender, EventArgs a)
        {
            Navigation.PushAsync(new PaginaUsuariosRegistrados());
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaUsuariosRegistrados());
        }
    }
}
