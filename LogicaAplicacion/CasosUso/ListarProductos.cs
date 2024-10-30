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
    public class ListarProductos : IListarProductos
    {
        public IRepositorioProducto repoProductos {  get; set; }
        public ListarProductos(IRepositorioProducto repo) 
        {
            repoProductos = repo;
        }

        public IEnumerable<ProductoDTO> GetProductos()
        {
            try
            {
                IEnumerable<Producto> ListaProductos = repoProductos.FindAll();

                IEnumerable<ProductoDTO> listaProductosDTO = ProductoMapper.ToListaProductoDTO(ListaProductos);

                return listaProductosDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer productos");
            }
        }
    }
}
