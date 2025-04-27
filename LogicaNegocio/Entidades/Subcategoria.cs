using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Subcategoria
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Categoria categoria { get; set; }
        public Subcategoria(string nombre, Categoria cat)
        {
            this.Nombre = nombre;
            this.categoria = cat;
        }
        public Subcategoria() { }
    }
}
