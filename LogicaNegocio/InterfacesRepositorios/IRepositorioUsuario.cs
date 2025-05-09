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
        Usuario BuscarPorEmail(string emailm,string contraseña);
        Usuario RegistrarCliente(string nombre, string apellido, string mail, bool esEstudiante);

        Usuario BuscarSoloEmail(string email);
    }
}
