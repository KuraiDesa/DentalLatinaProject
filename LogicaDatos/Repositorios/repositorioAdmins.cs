using LogicaDatos.EntityFramework;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class repositorioAdmins : IRepositorioAdmins
    {
        public LibreriaContext Context { get; set; }
        public repositorioAdmins(LibreriaContext context)
        {
            Context = context;
        }
        public void Add(Admin obj)
        {
            throw new NotImplementedException();
        }

        public Admin BuscarPorEmail(string email, string contraseña)
        {
            return Context.Admins
            .FirstOrDefault(u => u.us.mail == email && u.us.contraseña == contraseña);
        }

        public IEnumerable<Admin> FindAll()
        {
            throw new NotImplementedException();
        }

        public Admin FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Admin obj)
        {
            throw new NotImplementedException();
        }
    }
}
