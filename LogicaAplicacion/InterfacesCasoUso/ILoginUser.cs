using DentalLatina;
using System;
using System.Collections.Generic;
using DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.DTOs.UsuarioDTOs;

namespace LogicaAplicacion.InterfacesCasoUso
{
    public interface ILoginUser
    {
        public Usuario Login (LoguinUsuarioDTO loguinUsuarioDTO);
    }
}
