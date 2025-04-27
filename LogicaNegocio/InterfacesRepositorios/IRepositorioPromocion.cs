using DentalLatina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioPromocion :IRepositorio<Promocion>
    {
        Promocion BuscarPorNombre(string nombre);
    }
}
