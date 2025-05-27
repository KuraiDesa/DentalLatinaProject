using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IDetalleZona
    {
        ZonaDTO ZonabyId(int id);
        IEnumerable<ZonaDTO> getZonas();
        bool actualizarZona(ZonaDTO z);
    }
}
