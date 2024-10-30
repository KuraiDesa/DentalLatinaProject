using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class ProductoDTO
    {
        public string nombre { get; set; }
        public string photoUrl { get; set; }
        public string descripcion { get; set; }
        public string documentacion { get; set; }
        public int precio { get; set; }

        public ProductoDTO(string nombre, string photoUrl, string documentacion, string descripcion, int precio)
        {
            this.nombre = nombre;
            this.photoUrl = photoUrl;
            this.descripcion = descripcion;
            this.documentacion = documentacion;
        }
    }
}
