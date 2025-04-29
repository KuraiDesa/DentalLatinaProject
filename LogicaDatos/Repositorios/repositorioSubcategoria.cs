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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            if (Context.Entry(obj.categoria).State == EntityState.Detached)
            {
                Context.Attach(obj.categoria);
            }
            Context.Set<Subcategoria>().Add(obj);
            Context.SaveChanges(); ;
        }

        public IEnumerable<Subcategoria> FindAll()
        {
            return Context.Set<Subcategoria>()
              .ToList();
        }

        public Subcategoria FindById(int id)
        {
            Subcategoria subcat = Context.Subcategorias.Local.FirstOrDefault(c => c.Id == id);
            if (subcat == null)
            {
                subcat = Context.Subcategorias.AsNoTracking().FirstOrDefault(c => c.Id == id);
            }

            return subcat;
        }

        public IEnumerable<Subcategoria> FindByIdList(int? id)
        {
            var query = Context.Set<Subcategoria>().AsQueryable();

            if (id.HasValue && id.Value != -1)
            {
                query = query.Where(p => p.categoria.Id == id.Value);
            }

            return query.Include(p => p.categoria).ToList();
        }

        public void Remove(int id)
        {
            Subcategoria subcat = Context.Set<Subcategoria>().Find(id);

            if (subcat!=null)
            {
                Context.Set<Subcategoria>().Remove(subcat);
                Context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Error inesperado.");
            }
        }

        public void RemoveByCatId(int id)
        {
            IEnumerable<Subcategoria> subcat = FindByIdList(id);
            if (subcat.Any())
            {
                foreach(Subcategoria subcatId in subcat)
                {
                    Context.Set<Subcategoria>().Remove(subcatId);
                    Context.SaveChanges();
                }
            }
            else
            {
                return;

            }

        }

        public void Update(Subcategoria obj)
        {
            throw new NotImplementedException();
        }

    }
}
