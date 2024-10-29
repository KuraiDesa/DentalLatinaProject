using DentalLatina;
using LogicaDatos.EntityFramework;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class repositorioUsuario : IRepositorioUsuario
    {
        public LibreriaContext Context { get; set; }
        public repositorioUsuario(LibreriaContext context)
        {
            Context = context;
        }
        public List<Usuario> usuarios = new List<Usuario>();

        public void Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorEmail(string email, string contraseña)
        {
            return Context.Usuarios
           .FirstOrDefault(u => u.mail == email && u.contraseña == contraseña);
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
