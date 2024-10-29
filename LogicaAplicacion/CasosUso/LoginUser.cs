using LogicaNegocio.InterfacesRepositorios;
using LogicaAplicacion.InterfacesCasoUso;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio;
using DentalLatina;
using DTOs.DTOs.UsuarioDTOs;


namespace LogicaAplicacion.CasosUso
{
    public class LoginUser : ILoginUser
    {

        public IRepositorioUsuario Repo { get; set; }
        public LoginUser(IRepositorioUsuario repo)
        {
            this.Repo = repo;
        }
        public Usuario Login(LoguinUsuarioDTO loguinUsuarioDTO)
        {
            Usuario usEncontrado = Repo.BuscarPorEmail(loguinUsuarioDTO.mail, loguinUsuarioDTO.contraseña);
            if (usEncontrado != null)
            {
                return usEncontrado;
            }
            else
            {
                return null;
            }
        }

    }
}
