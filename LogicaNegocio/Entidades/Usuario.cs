using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLatina
{
    public class Usuario : IValidable
    {
        public string nombre {  get; set; }
        public string apellido { get; set;}
        [Key]
        public string mail { get; set;}
        public string contraseña {  get; set; }
        public bool estudiante {  get; set; }


        public Usuario(string nombre, string apellido, string mail, string contraseña, bool estudiante) {
            this.nombre = nombre;
            this.apellido = apellido;
            this.mail = mail;
            this.contraseña = contraseña;
            this.estudiante = estudiante;
        }       

        protected Usuario() { }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
