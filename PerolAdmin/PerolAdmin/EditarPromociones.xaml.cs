using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using PerolAdmin.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class EditarPromociones : ContentPage
    {
        public EditarPromociones()
        {
            InitializeComponent();
            btnlogearPromo.Clicked += BtnlogearPromo_Clicked;
            btnTomarFoto.Clicked += BtnTomarFoto_Clicked;
        }

        private async void BtnTomarFoto_Clicked(object sender, EventArgs e)
        {

          
        }

        private async void BtnlogearPromo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Promos());
        }
    }
}
