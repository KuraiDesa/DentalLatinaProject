using DentalLatina;
using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class ProductoMapper
    {
        public static Producto ToProducto (ProductoDTO prodDto)
        {
            Producto prod = new Producto(prodDto.nombre, prodDto.photoUrl, prodDto.categoria, prodDto.subcategoria, prodDto.documentacion, prodDto.descripcion, prodDto.precio);
            return prod;
        }

        public static ProductoDTO ToDTOProducto(Producto prod)
        {
            ProductoDTO productoDTO = new ProductoDTO(prod.nombre, prod.photoUrl, prod.categoria, prod.subcategoria, prod.documentacion, prod.descripcion, prod.precio);
            return productoDTO;
        }

        public static IEnumerable<ProductoDTO> ToListaProductoDTO(IEnumerable<Producto> prod)
        {
            List<ProductoDTO> prodDL = new List<ProductoDTO>();
            foreach (var producto in prod)
            {
                prodDL.Add(ToDTOProducto(producto));
            }
            return prodDL;
        }
    }
}
