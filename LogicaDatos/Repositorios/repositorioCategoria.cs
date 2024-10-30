using LogicaDatos.EntityFramework;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class repositorioCategoria : IRepositorioCategoria
    {
        public LibreriaContext Context { get; set; }
        public repositorioCategoria(LibreriaContext context)
        {
            Context = context;
        }
        public void Add(Categoria obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> FindAll()
        {
            throw new NotImplementedException();
        }

        public Categoria FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Categoria obj)
        {
            throw new NotImplementedException();
        }
    }
}
