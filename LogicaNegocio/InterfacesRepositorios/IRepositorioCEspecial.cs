using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioCEspecial : IRepositorio<CEspecial>
    {
        public IEnumerable<CEspecial> FindAllById(int? id);
    }
}
