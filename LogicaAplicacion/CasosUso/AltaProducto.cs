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
    public class AltaProducto : IAltaProducto
    {
        public IRepositorioProducto repoProd { get; set; }
        public AltaProducto(IRepositorioProducto repo)
        {
            repoProd = repo;
        }
        public void AltaProd(ProductoDTO prodDto)
        {
            try
            {
                Producto prod = ProductoMapper.ToProducto(prodDto);
                repoProd.Add(prod);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo dar de alta el producto");
            }
        }
    }
}
