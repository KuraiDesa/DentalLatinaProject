using LogicaNegocio.InterfacesRepositorios;
using LogicaAplicacion.InterfacesCasosUso;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio;
using DentalLatina;
using DTOs.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasoUso;
using LogicaNegocio.Entidades;


namespace LogicaAplicacion.CasosUso
{
    public class LoginUser : ILoginUser
    {

        public IRepositorioAdmins Repo { get; set; }
        public LoginUser(IRepositorioAdmins repo)
        {
            this.Repo = repo;
        }
        public Admin Login(LoguinUsuarioDTO loguinUsuarioDTO)
        {
            Admin usEncontrado = Repo.BuscarPorEmail(loguinUsuarioDTO.mail, loguinUsuarioDTO.contraseña);
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
