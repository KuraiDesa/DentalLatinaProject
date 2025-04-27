using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLatina
{
    public class Evento : IValidable
    {
        [Key]
        public int id;
        public string nombreEvento {  get; set; }
        public string descripcion { get; set; }
        public DateTime eventoFecha { get; set; }

        public Evento(string nombre, string descripcion, DateTime fecha) {
            this.nombreEvento = nombre;
            this.descripcion = descripcion;
            this.eventoFecha = fecha;
        }
        protected Evento() { }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
