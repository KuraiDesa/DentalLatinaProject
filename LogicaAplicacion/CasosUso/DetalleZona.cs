using DentalLatina;
using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class DetalleZona : IDetalleZona
    {
        public IRepositorioZona rep { get; set; }
        public DetalleZona(IRepositorioZona rep) 
        {
            this.rep = rep;
        }
        public IEnumerable<ZonaDTO> getZonas()
        {
            try
            {
                IEnumerable<Zona> zonas = rep.FindAll();
                if (zonas == null)
                {
                    return null;
                }
                IEnumerable<ZonaDTO> zD = ZonaMapper.ToDTOListZona(zonas);
                return zD;
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal");
            }
        }
        public ZonaDTO ZonabyId(int id)
        {
            try
            {
                Zona z = rep.FindById(id);
                if(z == null)
                {
                    return null;
                }
                ZonaDTO zD = ZonaMapper.ToDTOZona(z);
                return zD;
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal");
            }
           
        }
    }
}
