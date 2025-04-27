using DentalLatina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioZona : IRepositorio<Zona>
    {
        Zona BuscarPorZona(string Zona);
    }
}
