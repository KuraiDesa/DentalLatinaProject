using DentalLatina;
using DTOs.DTOs;
using DTOs.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IRegistroCliente
    {
        public Usuario RegistroClientes(RegistroUsuarioDTO registroUsuario);
        public Boolean buscoMail(string mail);
    }
}
