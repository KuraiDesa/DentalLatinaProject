using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLatina
{
    public class Zona: IValidable
    {
        public string zona {  get; set; }
        public int precio { get; set; }
        public string horario { get; set; }
        [Key]
        public int Id { get; set; }

        public Zona(string zona, string horario, int precio)
        {
            this.zona = zona;
            this.precio = precio;
            this.horario = horario;
        }

        protected Zona() { }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
