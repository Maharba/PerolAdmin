using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            TapGestureRecognizer clickConocenos = new TapGestureRecognizer();
            clickConocenos.Tapped += ClickConocenos_Tapped1;
            btnconocenos.GestureRecognizers.Add(clickConocenos);


            TapGestureRecognizer clickMenu= new TapGestureRecognizer();
            clickMenu.Tapped += ClickMenu_Tapped;
            btnMenuu.GestureRecognizers.Add(clickMenu);

            TapGestureRecognizer clickGaleria= new TapGestureRecognizer();
            clickGaleria.Tapped += ClickGaleria_Tapped;
            btngaleria.GestureRecognizers.Add(clickGaleria);


            TapGestureRecognizer clickPromo= new TapGestureRecognizer();
            clickPromo.Tapped += ClickPromo_Tapped;
            btnpromo.GestureRecognizers.Add(clickPromo);


            //TapGestureRecognizer clickRegistros = new TapGestureRecognizer();
            //clickRegistros.Tapped += ClickRegistros_Tapped;
            //btnventas.GestureRecognizers.Add(clickRegistros);

        }

        private async void ClickRegistros_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EnlaceVentas());
        
    }

        private async void ClickPromo_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaPromociones());
        }

        private async void ClickGaleria_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Galeria());
        }

        private async void ClickMenu_Tapped(object sender, EventArgs e)
      {
          await Navigation.PushAsync(new ListaPlatillos());
      }

         private async void ClickConocenos_Tapped1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Informacion());
        }

       
    }
}
