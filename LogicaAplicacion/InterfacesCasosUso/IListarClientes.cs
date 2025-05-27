using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IListarClientes
    {
        public IEnumerable<ClienteDTO> GetClientes();
        public IEnumerable<ClienteDTO> FilterClientes(string str);
    }
}
