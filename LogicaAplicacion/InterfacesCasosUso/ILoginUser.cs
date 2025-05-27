using DentalLatina;
using System;
using System.Collections.Generic;
using DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.DTOs.UsuarioDTOs;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.InterfacesCasoUso
{
    public interface ILoginUser
    {
        public Admin Login (LoguinUsuarioDTO loguinUsuarioDTO);

    }
}
