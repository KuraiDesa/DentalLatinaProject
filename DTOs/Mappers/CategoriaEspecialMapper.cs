using DTOs.DTOs;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class CategoriaEspecialMapper
    {
        public static CategoriaEspecialDTO ToDTOCategoriaEspecial(CEspecial categoria)
        {
            CategoriaEspecialDTO dto = new CategoriaEspecialDTO();
            dto.nombre = categoria.nombre;
            dto.id = categoria.id;
            
            return dto;
        }

        public static IEnumerable<CategoriaEspecialDTO> ToListaCategoriaEspecialDTO(IEnumerable<CEspecial> categoria)
        {
            List<CategoriaEspecialDTO> prodDL = new List<CategoriaEspecialDTO>();
            foreach (var cat in categoria)
            {
                prodDL.Add(ToDTOCategoriaEspecial(cat));
            }
            return prodDL;
        }
    }
}
