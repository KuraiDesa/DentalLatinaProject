using DentalLatina;
using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class AltaProducto
    {
        public IRepositorioProducto repoProd { get; set; }
        public AltaProducto(IRepositorioProducto repo)
        {
            repoProd = repo;
        }
    }
}
