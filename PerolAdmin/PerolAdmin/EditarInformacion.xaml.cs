using PerolAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class EditarInformacion : ContentPage
    {

        public string ID = "";

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            InfoPerol info = await App.AzureService.ObtenerInfoPerol();
            lblHistoria.Text = info.Historia;

            lblDireccion.Text = info.Direccion;
            lblTelefono.Text = info.Telefono;
            lblHorario.Text = info.Horario;

        }


        async void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {

                var display3 =
                    await DisplayAlert("AVISO", "¿Esta seguro de eliminar la Información", "Aceptar", "Cancelar");
                if (display3 == true)
                {
                    await App.AzureService.EliminarInfoPerol(ID);
                }
                else
                {
                    await DisplayAlert("AVISO", "¡Se Cancelo el Eliminar el Platillo!", "Ok");
                }

            }

            await Navigation.PopAsync();

        }

        async void btnGuardar_Click(object sender, EventArgs a)
        {

            string historia = lblHistoria.Text;
            string telefono = lblTelefono.Text;
            string direccion = lblDireccion.Text;

            string horario = lblHorario.Text;


            //await App.AzureService.ModificarInfoPerol(historia, telefono,  direccion, horario);
            //await Navigation.PopAsync();
            //        var displayy = await DisplayAlert("AVISO", "¿Deseas Agregar el Platillo?", "Aceptar", "Cancelar");  
            //    if (displayy == true)
            //    {
            //        await App.AzureService.AgregarInfoPerol(historia, telefono, direccion, horario);
            //    }
            //    else
            //    {
            //        await DisplayAlert("AVISO", "¡Se Cancelo el Agregar la Información!", "Ok");
            //    }
            //}
            //else
            //{
            var display2 =
                await DisplayAlert("AVISO", "¿Esta seguro de Modificar la Información?", "Aceptar", "Cancelar");
            if (display2 == true)
            {
                await App.AzureService.ModificarInfoPerol(historia, telefono, direccion, horario);
            }
            else
            {
                await DisplayAlert("AVISO", "¡Se Cancelo  Modificar la Información!", "Ok");
            }
            await Navigation.PopAsync();




        }

        public EditarInformacion()
        {
            InitializeComponent();
        }
    }

}