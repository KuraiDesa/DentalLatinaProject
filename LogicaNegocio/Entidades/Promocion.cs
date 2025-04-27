using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLatina
{
    public class Promocion : IValidable
    {
        public DateTime fecha { get; set; }
        public string titulo {  get; set; }
        public string descripcion { get; set;}
        [Key]
        public int Id { get; set; }

        public Promocion(string titulo, string descripcion, DateTime fecha)
        {
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha = fecha;
        }
        protected Promocion() { }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
