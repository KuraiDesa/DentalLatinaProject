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
        public static IEnumerable<ZonaDTO> ToDTOListZona(IEnumerable<Zona> zos)
        {
            List<ZonaDTO> zdto = new List<ZonaDTO>();
            foreach (Zona zo in zos)
            {

                zdto.Add(ToDTOZona2(zo));
            }
            return zdto;
        }
        public static ZonaDTO ToDTOZona2(Zona zo)
        {
            ZonaDTO zoDTO = new ZonaDTO();
            zoDTO.id = zo.Id;
            zoDTO.nombre = zo.zona;
            zoDTO.horario = zo.horario;
            zoDTO.precio = zo.precio;
            zoDTO.minimoDeEnvio = zo.minimoDeEnvio;
            return zoDTO;
        }
        public static Zona ToZona(ZonaDTO zo)
        {
            Zona zoE = new Zona(zo.nombre, zo.horario, zo.precio);
            zoE.Id = zo.id;
            zoE.minimoDeEnvio = zo.minimoDeEnvio;
            return zoE;
        }
    }
}

