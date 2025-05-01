using LogicaNegocio;

using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalLatina;

namespace DTOs.Mappers
{
    public class ProductoMapper
    {

        public static ProductoDTO ToDTOProducto(Producto prod)
        {
            ProductoDTO productoDTO = crearDTO(prod.Id,prod.nombre, prod.photoUrl, prod.documentacion, prod.descripcion, prod.precio);
            return productoDTO;
        }

        public static ProductoDTO crearDTO(int id, string nombre, string photoUrl, string documentacion, string descripcion, int precio)
        {
            ProductoDTO productoDTO = new ProductoDTO();
            productoDTO.id = id;
            productoDTO.nombre = nombre;
            productoDTO.photoUrl = photoUrl;
            productoDTO.descripcion = descripcion;
            productoDTO.documentacion = documentacion;
            productoDTO.precio = precio;
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
