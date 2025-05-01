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
    public class DetalleProducto : IDetalleProducto
    {
        public IRepositorioProducto rep {  get; set; }
        public DetalleProducto(IRepositorioProducto rep) 
        { 
            this.rep = rep;
        }
        public ProductoDTO detalleProducto(int id)
        {
            
                Producto pr = rep.FindById(id);
                if (pr == null)
                {
                    throw new Exception("No se encontro el producto");
                }
                ProductoDTO prDto = ProductoMapper.ToDTOProducto(pr);
                return prDto;
        }
        public IEnumerable<ProductoDTO> traerHasta4ProductoParecidos(int idProd) 
        { 
            IEnumerable<Producto> prodParecidos = rep.GetProductosRelacionados(idProd);
            if (prodParecidos.Count() == 0) {
                return null;
            }
            else
            {
                IEnumerable<ProductoDTO> prodParecidosDto = ProductoMapper.ToListaProductoDTO(prodParecidos);
                return prodParecidosDto;
            }
            
            
        }

    }
}
