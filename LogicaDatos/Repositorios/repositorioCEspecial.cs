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
    public class repositorioCEspecial : IRepositorioCEspecial
    {
        public LibreriaContext Context { get; set; }
        public repositorioCEspecial(LibreriaContext context)
        {
            Context = context;
        }
        public void Add(CEspecial obj)
        {
            Context.Set<CEspecial>().Add(obj);
            Context.SaveChanges();
        }

        public IEnumerable<CEspecial> FindAll()
        {
            return Context.Set<CEspecial>()
              .ToList();
        }

        public CEspecial FindById(int id)
        {
            CEspecial entity = Context.CEspecial.Local.FirstOrDefault(c => c.id == id);

            if (entity == null)
            {
                entity = Context.CEspecial.AsNoTracking().FirstOrDefault(c => c.id == id);
            }

            return entity;
        }

        public void Remove(int id)
        {
            CEspecial cat = Context.Set<CEspecial>().Find(id);
            if (cat != null)
            {
                Context.Set<CEspecial>().Remove(cat);
                Context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Error inesperado");
            }
        }

        public void Update(CEspecial obj)
        {
            throw new NotImplementedException();
        }
    }
}
