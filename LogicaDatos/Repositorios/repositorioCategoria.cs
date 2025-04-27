using DentalLatina;
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
    public class repositorioCategoria : IRepositorioCategoria
    {
        public LibreriaContext Context { get; set; }
        public repositorioCategoria(LibreriaContext context)
        {
            Context = context;
        }
        public void Add(Categoria obj)
        {
            Context.Set<Categoria>().Add(obj);
            Context.SaveChanges();
        }

        public IEnumerable<Categoria> FindAll()
        {
            return Context.Set<Categoria>()
              .ToList();
        }

        public Categoria FindById(int id)
        {
            Categoria entity = Context.Categorias.Local.FirstOrDefault(c => c.Id == id);

            if (entity == null)
            {
                entity = Context.Categorias.AsNoTracking().FirstOrDefault(c => c.Id == id);
            }

            return entity;
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
