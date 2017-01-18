using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PerolAdmin.Models
{
    public class AzureDataServices
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<Platillos> tablaPlatillos;
        IMobileServiceSyncTable<Categorias> tablaCategorias;
        IMobileServiceSyncTable<InfoPerol> tablaInfoPerol;
        IMobileServiceSyncTable<Usuarios> tablaUsuario;
        IMobileServiceSyncTable<Reservaciones> tablaReservacions;
        IMobileServiceSyncTable<Galeria> tablaGaleria;
        IMobileServiceSyncTable<Promociones> tablaPromocines;

        bool isInitialized;

        public async Task Initialize()
        {
            if (isInitialized)
                return;
            MobileService = new MobileServiceClient("http://perolito-mqz-app.azurewebsites.net");
            const string path = "syncstore-perolito-mqz.db";

            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<Platillos>();
            store.DefineTable<Categorias>();
            store.DefineTable<InfoPerol>();
            store.DefineTable<Usuarios>();
            store.DefineTable<Reservaciones>();
            store.DefineTable<Galeria>();
            store.DefineTable<Promociones>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            tablaPlatillos = MobileService.GetSyncTable<Platillos>();
            tablaCategorias = MobileService.GetSyncTable<Categorias>();
            tablaInfoPerol = MobileService.GetSyncTable<InfoPerol>();
            tablaUsuario = MobileService.GetSyncTable<Usuarios>();
            tablaReservacions = MobileService.GetSyncTable<Reservaciones>();
            tablaGaleria = MobileService.GetSyncTable<Galeria>();
            tablaPromocines = MobileService.GetSyncTable<Promociones>();

            isInitialized = true;

        }


        //////////////////////////////////////////Promociones///////////////////////////////////
        public async Task<IEnumerable<Promociones>> ObtenerPromociones()
        {
            await Initialize();
            await SyncPromociones();
            return await tablaPromocines.OrderBy(a => a.Nombre).ToEnumerableAsync();
        }

        public async Task<Promociones> ObtenerPromocion(string id)
        {
            await Initialize();
            await SyncPromociones();
            return (await tablaPromocines.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }

        public async Task<Promociones> AgregarPromocion(string nombre, string descripcion,  string dia, string Urlimagenn)
        {
            await Initialize();
            var item = new Promociones
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Dia = dia,
                urlImagen = Urlimagenn
            };

            await tablaPromocines.InsertAsync(item);
            await SyncPromociones();
            return item;
        }


        public async Task<Promociones> ModificarPromocion(string id, string nombre, string descripcion, string dia, string Urlimageen)
        {
            await Initialize();
            var item = await ObtenerPromocion(id);
            item.Nombre = nombre;
            item.Descripcion = descripcion;
            item.Dia = dia;
            item.urlImagen = Urlimageen;

            await tablaPromocines.UpdateAsync(item);
            await SyncPromociones();
            return item;
        }

        public async Task EliminarPromocion(string id)
        {
            await Initialize();
            var item = await ObtenerPromocion(id);
            await tablaPromocines.DeleteAsync(item);
            await SyncPromociones();
        }

        private async Task SyncPromociones()
        {
            await tablaPromocines.PullAsync("Promociones", tablaPromocines.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }

        public async Task<Promociones> GuardarUrlPromociones(string imag)
        {
            await Initialize();

            var item = new Promociones()
            {
                urlImagen = imag

            };
            await tablaPromocines.InsertAsync(item);
            await SyncPromociones();
            return item;
        }





        ///////////////////////////////////////Galeria//////////////////
        public async Task EliminarunaFotodeGaleria(string id)
        {
            await Initialize();
            var item = await ObtenerunaFotodeGaleria(id);
            await tablaGaleria.DeleteAsync(item);
            await SyncGaleria();

        }
        public async Task<Galeria> ObtenerunaFotodeGaleria(string id)
        {
            await Initialize();
            await SyncGaleria();
            return (await tablaGaleria.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }


        public async Task<Galeria> GuardarUrlGaleria(string imag)
        {
            await Initialize();

            var item = new Galeria()
            {
                urlimagen  = imag

            };
            await tablaGaleria.InsertAsync(item);
            await SyncGaleria();
            return item;
        }

        private async Task SyncGaleria()
        {
            await tablaGaleria.PullAsync("Galeria", tablaGaleria.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }


        public async Task<IEnumerable<Galeria>> ObtenerFotosdeGaleria()
        {
            await Initialize();
            await SyncGaleria();
            return await tablaGaleria.OrderBy(a => a.urlimagen).ToEnumerableAsync();
        }
    



    //////////////////////////////////Tabla Platillos//////////////////////////////
        public async Task<IEnumerable<Platillos>> ObtenerPlatillos()
        {
            await Initialize();
            await SyncPlatillos();
            return await tablaPlatillos.OrderBy(a => a.Nombre).ToEnumerableAsync();
        }

        public async Task<Platillos> ObtenerPlatillo(string id)
        {
            await Initialize();
            await SyncPlatillos();
            return (await tablaPlatillos.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }

        

        public async Task<Platillos> AgregarPlatillos(string nombre, string descripcion, decimal precio, string categoria, string urlImagen)
        {
            await Initialize();

            var item = new Platillos
            {
                Nombre = nombre,
                Descripciom = descripcion,
                Precio = precio,

                Categorias = categoria, 
               urlImagen = urlImagen
              
            };
            await tablaPlatillos.InsertAsync(item);
            await SyncPlatillos();
            return item;
        }

        public async Task<Platillos> ModificarPlatillo(string id, string nombre, string descripcion, decimal precio, string categoria, string urlImagen)
        {
            await Initialize();
            var item = await ObtenerPlatillo(id);
            item.Nombre = nombre;
            item.Descripciom = descripcion;
            item.Precio = precio;
            item.Categorias = categoria;
            item.urlImagen = urlImagen;

            await tablaPlatillos.UpdateAsync(item);
            await SyncPlatillos();
            return item;
        }

        public async Task EliminarPlatillo(string id)
        {
            await Initialize();
            var item = await ObtenerPlatillo(id);
            await tablaPlatillos.DeleteAsync(item);
            await SyncPlatillos();
        }

        private async Task SyncPlatillos()
        {
            await tablaPlatillos.PullAsync("Platillos", tablaPlatillos.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }



        //////////////////////////////////Tabla Usuarios//////////////////////////////
        public async Task<IEnumerable<Usuarios>> ObtenerUsuarios()
        {
            await Initialize();
            await SyncUsuario();
            return await tablaUsuario.OrderBy(a => a.Nombre).ToEnumerableAsync();
        }

        public async Task<Usuarios> ObtenerUsuario(string id)
        {
            await Initialize();
            await SyncUsuario();
            return (await tablaUsuario.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }


        public async Task<Usuarios> AgregarUsuarios(string nombre, string apellido, string telefono, string direccion, string correo, string contraseña)
        {
            await Initialize();

            var item = new Usuarios()
            {
                Nombre = nombre,
                Apellido = apellido,
                
                Telefono = telefono,
                Direccion = direccion,
                Correo = correo,
                Contraseña = contraseña,
              
            };
            await tablaUsuario.InsertAsync(item);
            await SyncUsuario();
            return item;
        }

        public async Task<Usuarios> ModificarUsuario(string id, string nombre, string apellido,  string telefono, string direccion, string correo, string contraseña)
        {
            await Initialize();
            var item = await ObtenerUsuario(id);
            item.Nombre = nombre;
            item.Apellido = apellido;
           
            item.Telefono = telefono;
            item.Direccion = direccion;
            item.Correo = correo;
            item.Contraseña = contraseña;

           
            await tablaUsuario.UpdateAsync(item);
            await SyncUsuario();
            return item;
        }

        public async Task EliminarUsuario(string id)
        {
            await Initialize();
            var item = await ObtenerUsuario(id);
            await tablaUsuario.DeleteAsync(item);
            await SyncUsuario();
        }
       
        private async Task SyncUsuario()
        {
          
            await tablaUsuario.PullAsync("Usuarios", tablaUsuario.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }

        ////////////////////////////////Tabla Categoria/////////////////////////////////////
        public async Task<IEnumerable<Categorias>> ObtenerCategorias()
        {
            await Initialize();
            await SyncCategorias();
            return await tablaCategorias.OrderBy(a => a.Nombre).ToEnumerableAsync();
        }
        public async Task<Categorias> ObtenerCategoria(string id)
        {
            await Initialize();
            await SyncCategorias();
            return (await tablaCategorias.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }

        public async Task<Categorias> AgregarCategorias(string nombre, string urlImage)
        {
            await Initialize();

            var item = new Categorias()
            {
                Nombre = nombre,
                urlImagen = urlImage

            };
            await tablaCategorias.InsertAsync(item);
            await SyncCategorias();
            return item;
        }

        public async Task<Categorias> ModificarCategoria(string id, string nombre, string urlimag)
        {
            await Initialize();
            var item = await ObtenerCategoria(id);
            item.Nombre = nombre;
            item.urlImagen = urlimag;

            await tablaCategorias.UpdateAsync(item);
            await SyncCategorias();
            return item;
        }

        public async Task EliminarCategoria(string id)
        {
            await Initialize();
            var item = await ObtenerCategoria(id);
            await tablaCategorias.DeleteAsync(item);
            await SyncCategorias();

        }

        private async Task SyncCategorias()
        {
            await tablaCategorias.PullAsync("Categorias", tablaCategorias.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }

     



        /////////////////////////////Informacionn del perol/////////////////////////(////////////////////////

        public async Task<InfoPerol> ObtenerInfoPerol()
        {
            await Initialize();
            await SyncInformacionPerol();
            var info = await (from i in tablaInfoPerol
                              select i).ToListAsync();
            return info.FirstOrDefault();
        }

        public async Task<IEnumerable<InfoPerol>> ObtenerInfoPeroool()
        {
            await Initialize();
            await SyncInformacionPerol();
            return await tablaInfoPerol.OrderBy(a => a.Historia).ToEnumerableAsync();
        }

        public async Task<InfoPerol> ObtenerInfoPerolUna(string id)
        {
            await Initialize();
            await SyncInformacionPerol();
            
            return (await tablaInfoPerol.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
          
        }



        public async Task<InfoPerol> AgregarInfoPerol(string historia, string telefono,  string direccion,  string horario)
        {
            await Initialize();

            var item = new InfoPerol()
            {
                Historia = historia,
                Telefono = telefono,
                
                Direccion = direccion,
                
                Horario = horario
            };
            await tablaInfoPerol.InsertAsync(item);
            await SyncInformacionPerol();
            return item;
        }

        public async Task ModificarInfoPerol(string historia, string telefono, 
            string direccion,  string horario)
        {
            await Initialize();
            var modif = (await (from a in tablaInfoPerol
                                select a).ToListAsync()).FirstOrDefault();
            modif.Historia = historia;
            modif.Telefono = telefono;
            
            modif.Direccion = direccion;
            
            modif.Horario = horario;


            await tablaInfoPerol.UpdateAsync(modif);
            await SyncInformacionPerol();

        }

        public async Task EliminarInfoPerol(string id)
        {
            await Initialize();
            var item = await ObtenerInfoPerol();
            await tablaInfoPerol.DeleteAsync(item);
            await SyncInformacionPerol();
        }


        private async Task SyncInformacionPerol()
        {
            await tablaInfoPerol.PullAsync("InfoPerol", tablaInfoPerol.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }


    //////////////////////////////////Tabla RESERVACIONES//////////////////////////////
    public async Task<IEnumerable<Reservaciones>> ObtenerReservaciones()
    {
        await Initialize();
        await SyncReservaciones();
        return await tablaReservacions.OrderBy(a => a.Fecha).ToEnumerableAsync();
    }

    public async Task<Reservaciones> ObtenerReservacion(string id)
    {
        await Initialize();
        await SyncReservaciones();
        return (await tablaReservacions.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
    }


    public async Task<Reservaciones> AgregarReservaciones(int mesa, int personas, DateTime fehca)
    {
        await Initialize();

        var item = new Reservaciones()
        {
            Mesa = mesa,

            Personas = personas,
            Fecha = fehca,



        };
        await tablaReservacions.InsertAsync(item);
        await SyncReservaciones();
        return item;
    }

    public async Task<Reservaciones> ModificarReservvaciones(string id, int mesa, int personas, DateTime fecha)
    {
        await Initialize();
        var item = await ObtenerReservacion(id);
        item.Mesa = mesa;
        item.Personas = personas;
        item.Fecha = fecha;

        await tablaReservacions.UpdateAsync(item);
        await SyncReservaciones();
        return item;
    }

    public async Task EliminarReservaciones(string id)
    {
        await Initialize();
        var item = await ObtenerReservacion(id);
        await tablaReservacions.DeleteAsync(item);
        await SyncReservaciones();
    }

    private async Task SyncReservaciones()
    {
        await tablaReservacions.PullAsync("Reservaciones", tablaReservacions.CreateQuery());
        await MobileService.SyncContext.PushAsync();
    }
}
}
