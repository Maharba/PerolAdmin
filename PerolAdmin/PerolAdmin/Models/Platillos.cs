using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerolAdmin.Models
{
   public class Platillos
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public string Nombre { get; set; }
        public string Descripciom { get; set; }
        public decimal Precio { get; set; }
        public string urlImagen { get; set; }

        //enlaze
        public string Categorias { get; set; }
    }
}
