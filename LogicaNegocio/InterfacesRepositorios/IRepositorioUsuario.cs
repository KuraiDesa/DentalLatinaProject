using DentalLatina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        
        Usuario RegistrarCliente(string nombre, string apellido, string mail, bool esEstudiante);
        public IEnumerable<Usuario> BuscarPorNombre(string nombre);
        public IEnumerable<Usuario> BuscarPorApellido(string apellido);
        public IEnumerable<Usuario> BuscarPorEmail(string email);
        Usuario BuscarSoloEmail(string email);
    }
}
