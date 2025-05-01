using DentalLatina;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioProducto : IRepositorio<Producto>
    {
        IEnumerable<Producto> BuscarPorNombre(string nombre);
        IEnumerable<Producto> BuscarPorNombreCategoria(int? id, string nombre);
        IEnumerable<Producto> BuscarPorCategoria(int id);

        IEnumerable<Producto> GetProductosRelacionados(int productoId);
        void RemoveByCatId(int id);
        void RemoveByScatId(int id);
    }
}
