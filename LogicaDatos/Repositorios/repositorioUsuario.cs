using DentalLatina;
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
    public class repositorioUsuario : IRepositorioUsuario
    {
        public LibreriaContext Context { get; set; }
        public repositorioUsuario(LibreriaContext context)
        {
            Context = context;
        }

        public void Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorEmail(string email, string contraseña)
        {
            return Context.Usuarios
           .FirstOrDefault(u => u.mail == email && u.contraseña == contraseña);
        }
        public Usuario BuscarSoloEmail(string email)
        {
            return Context.Usuarios
           .FirstOrDefault(u => u.mail == email);
        }
        public IEnumerable<Usuario> FindAll()
        {
            return Context.Set<Usuario>()
             .ToList();
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

        public Usuario RegistrarCliente(string nombre, string apellido, string mail, bool esEstudiante)
        {
            try
            {
                if (BuscarSoloEmail(mail) == null)
                {
                    Usuario us = new Usuario(nombre, apellido, mail, "", esEstudiante);
                    Context.Usuarios.Add(us);

                    // Guarda los cambios en la base de datos
                    Context.SaveChanges();

                    return us;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
    }
}
