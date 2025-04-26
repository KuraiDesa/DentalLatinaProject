using LogicaDatos.EntityFramework;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class repositorioSubcategoria : IRepositorioSubcategoria
    {
        public LibreriaContext Context { get; set; }
        public repositorioSubcategoria(LibreriaContext context)
        {
            Context = context;
        }
        public void Add(Subcategoria obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subcategoria> FindAll()
        {
            return Context.Set<Subcategoria>()
              .ToList();
        }

        public Subcategoria FindById(int id)
        {
            Subcategoria entity = Context.Subcategorias.Local.FirstOrDefault(c => c.Id == id);
            if (entity == null)
            {
                entity = Context.Subcategorias.AsNoTracking().FirstOrDefault(c => c.Id == id);
            }

            return entity;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Subcategoria obj)
        {
            throw new NotImplementedException();
        }
    }
}
