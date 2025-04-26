using DentalLatina;
using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class ListarClientes : IListarClientes
    {
        public IRepositorioUsuario repoUsuarios { get; set; }
        public ListarClientes(IRepositorioUsuario repo)
        {
            this.repoUsuarios = repo;
        }
        public IEnumerable<ClienteDTO> GetClientes()
        {
            try
            {
                IEnumerable<Usuario> ListaClientes = repoUsuarios.FindAll();

                IEnumerable<ClienteDTO> listaClientesDTO = UsuarioMapper.ToListaClientesDTO(ListaClientes);

                return listaClientesDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer categoria");
            }
        }
    }
}
