using LogicaNegocio.Entidades;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DentalLatina
{
    public class Producto : IValidable
    {
        [Key]
        public int Id { get; set; }
        public string nombre { get; set; }
        public string photoUrl { get; set; }
        public string descripcion { get; set; }
        public Categoria categoria { get; set; }
        public  Subcategoria subcategoria { get; set; }
        public string documentacion { get; set; }
        public int precio { get; set; }
        

        public Producto(string nombre, string photoUrl, Categoria categoria, Subcategoria subcategoria,string documentacion, string descripcion, int precio)
        {
            this.nombre = nombre;
            this.photoUrl = photoUrl;
            this.descripcion = descripcion;
            this.categoria = categoria;
            this.subcategoria = subcategoria;
            this.documentacion = documentacion;
            //haccer un IValidate
            try
            {
                this.precio = precio;
            }
            catch (FormatException)
            {
                //Hay que ver que hacer aca
            }
            catch (Exception ex)
            {

            }
        }
        public Producto() { }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
