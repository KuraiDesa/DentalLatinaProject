using DentalLatina;
using DTOs.DTOs;
using DTOs.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class RegistroCliente : IRegistroCliente
    {
        public IRepositorioUsuario Repo { get; set; }
        public RegistroCliente(IRepositorioUsuario repo)
        {
            this.Repo = repo;
        }

        public Boolean buscoMail(string mail)
        {
            Usuario usu = Repo.BuscarSoloEmail(mail);
            if (usu == null) {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Usuario RegistroClientes(RegistroUsuarioDTO registroUsuario)
        {
            Usuario usEncontrado = Repo.BuscarPorEmail(registroUsuario.email,null);
            if (usEncontrado == null)
            {
                try
                {
                   return Repo.RegistrarCliente(registroUsuario.nombre, registroUsuario.apellido, registroUsuario.email, registroUsuario.esEstudiante);
                
                }
                catch (Exception ex)
                {
                    return null;
                }
               
            
            }
            else
            {
                return null;
            }
         
        }
    }
}
