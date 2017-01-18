using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerolAdmin.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Syncfusion.SfCarousel.XForms;

namespace PerolAdmin
{
    public partial class ListaPromociones : ContentPage
    {
        private ObservableCollection<SfCarouselItem> _imagenes;
        public ListaPromociones()
        {
            InitializeComponent();
           
        }
        protected async override void OnAppearing()
        {

            base.OnAppearing();

            var cargando = UserDialogs.Instance.Loading("Cargando...");
            lsvPromociones.ItemsSource = await App.AzureService.ObtenerPromociones();
            cargando.Hide();

        }

        private void lsvPromociones_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                Promociones promo = e.SelectedItem as Promociones;
                PaginaPromociones pagina = new PaginaPromociones();
                pagina.ID = promo.Id;
                Navigation.PushAsync(pagina);

            }
        }
        
        private void btnNuevo_Click(object sender, EventArgs a)
        {
            Navigation.PushAsync(new PaginaPromociones());
        }

    }
}
