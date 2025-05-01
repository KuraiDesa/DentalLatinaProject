using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IDetalleProducto
    {
        ProductoDTO detalleProducto(int id);
        IEnumerable<ProductoDTO> traerHasta4ProductoParecidos(int id);
    }
}
