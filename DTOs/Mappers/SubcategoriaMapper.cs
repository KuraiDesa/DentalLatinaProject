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
    public class SubategoriaMapper
    {
        public static SubcategoriaDTO ToDTOSubcategoria(Subcategoria categoria)
        {
            SubcategoriaDTO dto = new SubcategoriaDTO();
            dto.nombre = categoria.Nombre;
            dto.id = categoria.Id;
            return dto;
        }

        public static IEnumerable<SubcategoriaDTO> ToListaSubcategoriaDTO(IEnumerable<Subcategoria> categoria)
        {
            List<SubcategoriaDTO> prodDL = new List<SubcategoriaDTO>();
            foreach (var cat in categoria)
            {
                prodDL.Add(ToDTOSubcategoria(cat));
            }
            return prodDL;
        }
    }
}
