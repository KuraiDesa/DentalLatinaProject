using DentalLatina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Admin
    {
        public int id { get; set; }
        public Usuario us {  get; set; }

        //sin constructor por seguridad
    }
}
