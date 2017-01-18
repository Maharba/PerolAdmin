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
    public partial class ListaCategorias : ContentPage
    {
        public ListaCategorias()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var cargando = UserDialogs.Instance.Loading("Cargando...");
            lsvCategorias.ItemsSource = await App.AzureService.ObtenerCategorias();
            cargando.Hide();
        }

        private void lsvCategorias_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Categorias categoria = e.SelectedItem as Categorias;
                PaginaCategoria pagina = new PaginaCategoria();
                pagina.ID = categoria.Id;
                Navigation.PushAsync(pagina);

            }
        }

        private void btnNuevo_Click(object sender, EventArgs a)
        {
            Navigation.PushAsync(new PaginaCategoria());
        }
    }
}
