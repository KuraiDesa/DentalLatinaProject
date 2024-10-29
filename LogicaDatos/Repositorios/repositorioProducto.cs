using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalLatina;
using LogicaNegocio.InterfacesRepositorios;

namespace LogicaDatos.Repositorios
{
    public class repositorioProducto : IRepositorioProducto
    {
        public void Add(Producto obj)
        {
            throw new NotImplementedException();
        }

        public Producto BuscarPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public Producto BuscarPorPrecio(int precio)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> FindAll()
        {
            throw new NotImplementedException();
        }

        public Producto FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Producto obj)
        {
            throw new NotImplementedException();
        }
    }
}
