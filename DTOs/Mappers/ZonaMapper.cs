using DentalLatina;
using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class ZonaMapper
    {
        public static ZonaDTO ToDTOZona(Zona zo)
        {
            ZonaDTO zoDTO = new ZonaDTO();
            zoDTO.nombre = zo.zona;
            zoDTO.horario = zo.horario;
            zoDTO.precio = zo.precio;
            zoDTO.minimoDeEnvio = zo.minimoDeEnvio;
            return zoDTO;
        }
    }
}
