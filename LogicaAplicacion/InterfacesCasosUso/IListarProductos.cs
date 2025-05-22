using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IListarProductos
    {
        public IEnumerable<ProductoDTO> GetProductos();
        IEnumerable<ProductoDTO> ListarProductosCategoria(int id);
        IEnumerable<ProductoDTO> ListarProductosSubcategoria(int id);
        IEnumerable<ProductoDTO> ListarProductosCategoriaEspecial(int id);
        IEnumerable<ProductoDTO> ListarProductosNombre(string nombre);
        IEnumerable<ProductoDTO> ListarPorCateNombre(int id, string nombre);
    }
}
