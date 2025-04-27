using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class DetalleZona : IDetalleZona
    {
        public IRepositorioZona rep { get; set; }
        public DetalleZona(IRepositorioZona rep) 
        {
            this.rep = rep;
        }
        public ZonaDTO Zona(int id)
        {
            throw new NotImplementedException();
        }
    }
}
