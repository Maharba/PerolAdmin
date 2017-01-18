using Acr.UserDialogs;
using PerolAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class ListaPlatillos : ContentPage
    {
        public ListaPlatillos()
        {
            InitializeComponent();
            
        }
        
        protected async override void OnAppearing()
        {
            
            base.OnAppearing();

            var cargando = UserDialogs.Instance.Loading("Cargando...");
            lsvPlatillos.ItemsSource = await App.AzureService.ObtenerPlatillos();
            cargando.Hide();

        }

        private void lsvPlatillos_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                
                Platillos platillo = e.SelectedItem as Platillos;
                PaginaPlatillo pagina = new PaginaPlatillo();
                pagina.ID = platillo.Id;
                Navigation.PushAsync(pagina);

            }
        }

        private void btnNuevo_Click(object sender, EventArgs a)
        {
            Navigation.PushAsync(new PaginaPlatillo());
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaCategorias());
        }
    }
}
