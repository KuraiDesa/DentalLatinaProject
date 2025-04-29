using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class CEspecial
    {
        [Key]
        public int id {  get; set; }
        public string nombre { get; set; }

        public CEspecial(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
