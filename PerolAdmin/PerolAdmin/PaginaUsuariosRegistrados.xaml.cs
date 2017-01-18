using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerolAdmin.Models;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace PerolAdmin
{
    public partial class PaginaUsuariosRegistrados : ContentPage
    {
        public PaginaUsuariosRegistrados()
        {
            InitializeComponent();
        }

      
        public string ID = "";

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (ID != "")
            {
                Usuarios usuo = await App.AzureService.ObtenerUsuario(ID);
                txtNombre.Text = usuo.Nombre;
                txtApellido.Text = usuo.Apellido;
                txtTelefono.Text = usuo.Telefono;
                txtDireccion.Text = usuo.Direccion;
                txtCorreo.Text = usuo.Correo;
                txtContraseña.Text = usuo.Contraseña;
    
                   
            }
        }
        
        async void btnGuardar_Click(object sender, EventArgs a)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;
            string correo = txtCorreo.Text;
            string contraseña = txtContraseña.Text;
           

            if (ID == String.Empty)
                await App.AzureService.AgregarUsuarios(nombre,apellido,telefono,direccion,correo,contraseña);
            else
                await App.AzureService.ModificarUsuario(ID, nombre, apellido,telefono,direccion, correo, contraseña);
            await Navigation.PopAsync();

        }

        async void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {
                await App.AzureService.EliminarUsuario(ID);
                await Navigation.PopAsync();
            }
        }
    }
}
