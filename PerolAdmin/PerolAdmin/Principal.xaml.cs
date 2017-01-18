using PerolAdmin.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PerolAdmin
{
    public partial class Principal : MasterDetailPage
    {
        public Principal()
        {




            InitializeComponent();
            Detail = new NavigationPage(new Inicio());
            lstView.ItemTapped += LstView_ItemTapped;
            lstView.ItemsSource = new ObservableCollection<ClassMenuItem>
                {
                new ClassMenuItem() { Imagen="icon5.png", Texto ="Inicio"},
                new ClassMenuItem() { Imagen="icon8.png", Texto ="Menú"},
                new ClassMenuItem() { Imagen="icon.png", Texto ="Conócenos"},
                new ClassMenuItem() { Imagen="galeria.png", Texto ="Galería"},
                new ClassMenuItem() { Imagen="icon3.png", Texto ="Contáctanos"},
                new ClassMenuItem() { Imagen="icon2.png", Texto ="Reservación"},
                new ClassMenuItem() { Imagen="icon9.png", Texto ="Promociones"},
                new ClassMenuItem() { Imagen="icono13.png", Texto ="Ayuda y Comentarios"},
                new ClassMenuItem() { Imagen="ayudaComentarios.png", Texto ="Acerca de"}
            };
            lstView.ItemSelected += LstView_ItemSelected;
        }

        private void LstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void LstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ClassMenuItem menu = (ClassMenuItem)e.Item;
            if (menu.Texto == "Inicio")
            {

                this.Detail = new NavigationPage(new Inicio());
                this.IsPresented = false;

            }
            else if (menu.Texto == "Menú")
            {
                //this.Detail = new NavigationPage(new CategoriasPlatillos());
                //this.IsPresented = false;
            }
            else if (menu.Texto == "Conócenos")
            {
                //this.Detail = new NavigationPage(new Conocenos());
                //this.IsPresented = false;
            }
            else if (menu.Texto == "Galería")
            {
                //this.Detail = new NavigationPage(new Galeria());
                //this.IsPresented = false;
            }
            else if (menu.Texto == "Contáctanos")
            {
                //this.Detail = new NavigationPage(new Contactanos());
                //this.IsPresented = false;
            }
            else if (menu.Texto == "Reservación")
            {
                //this.Detail = new NavigationPage(new LugaresReservar());
                //this.IsPresented = false;
            }
            else if (menu.Texto == "Promociones")
            {
                //this.Detail = new NavigationPage(new RotatorPromos());
                //this.IsPresented = false;
            }
            else if (menu.Texto == "Ayuda y Comentarios")
            {
                //this.Detail = new NavigationPage(new ayudaComentarios());
                //this.IsPresented = false;
            }
            else if (menu.Texto == "Acerca de")
            {
                //this.Detail = new NavigationPage (new acercaDe());
                //this.IsPresented = false;
            }
        }
    }
}
