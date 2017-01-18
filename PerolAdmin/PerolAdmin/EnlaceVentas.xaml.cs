using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class EnlaceVentas : ContentPage
    {
        public EnlaceVentas()
        {
            InitializeComponent();
            btnListaUsuarios.Clicked += BtnListaUsuarios_Clicked;
        }

        private async void BtnListaUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaUsuariosRegistrados());

        }
    }
}
