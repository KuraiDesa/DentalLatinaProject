using DentalLatina;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioAdmins:IRepositorio<Admin>
    {
        Admin BuscarPorEmail(string email, string contraseña);

    }
}
