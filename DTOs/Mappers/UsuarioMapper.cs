using DentalLatina;
using DTOs.DTOs;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class UsuarioMapper
    {
        public static ClienteDTO ToDTOCliente(Usuario usu)
        {
            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.nombre = usu.nombre;
            clienteDTO.mail = usu.mail;
            clienteDTO.apellido = usu.apellido;
           
            return clienteDTO;
        }
        public static IEnumerable<ClienteDTO> ToListaClientesDTO(IEnumerable<Usuario> usuarios)
        {
            List<ClienteDTO> clienteDTO = new List<ClienteDTO>();
            foreach (var cliente in usuarios)
            {
                clienteDTO.Add(ToDTOCliente(cliente));
            }
            return clienteDTO;
        }
    }
}
