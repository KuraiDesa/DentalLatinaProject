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
    public class CategoriaMapper
    {
        public static CategoriaDTO ToDTOCategoria(Categoria categoria)
        {
            CategoriaDTO dto = new CategoriaDTO();
            dto.nombre = categoria.Nombre;
            dto.id = categoria.Id;
            return dto;
        }
        public static Categoria ToCategoria(CategoriaDTO categoria)
        {
            Categoria cat = new Categoria();
            cat.Nombre = categoria.nombre;
            cat.Id = categoria.id;
            return cat;
        }
        public static IEnumerable<CategoriaDTO> ToListaCategoriaDTO(IEnumerable<Categoria> categoria)
        {
            List<CategoriaDTO> prodDL = new List<CategoriaDTO>();
            foreach (var cat in categoria)
            {
                prodDL.Add(ToDTOCategoria(cat));
            }
            return prodDL;
        }
    }
}
