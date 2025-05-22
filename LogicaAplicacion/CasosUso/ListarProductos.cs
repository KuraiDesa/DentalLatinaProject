using DentalLatina;
using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaDatos.Repositorios;
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

        public IEnumerable<ProductoDTO> ListarProductosCategoria(int id)
        {
            return ProductoMapper.ToListaProductoDTO(repoProductos.BuscarPorCategoria(id));
        }
        public IEnumerable<ProductoDTO> ListarProductosSubcategoria(int id)
        {
            return ProductoMapper.ToListaProductoDTO(repoProductos.BuscarPorSubcategoria(id));
        }
        public IEnumerable<ProductoDTO> ListarProductosCategoriaEspecial(int id)
        {
            return ProductoMapper.ToListaProductoDTO(repoProductos.BuscarPorCategoriaEspecial(id));
        }
        public IEnumerable<ProductoDTO> ListarProductosNombre(string nombre)
        {
            return ProductoMapper.ToListaProductoDTO(repoProductos.BuscarPorNombre(nombre)).OrderBy(p => p.nombre);
        }
        public IEnumerable<ProductoDTO> ListarPorCateNombre(int id, string nombre)
        {
            return ProductoMapper.ToListaProductoDTO(repoProductos.BuscarPorNombreCategoria(id, nombre)).OrderBy(p => p.nombre);
        }
    }
}
