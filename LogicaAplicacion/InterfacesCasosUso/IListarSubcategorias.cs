using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IlistarSubcategorias
    {
        public IEnumerable<SubcategoriaDTO> GetSubcategoria();
        public IEnumerable<SubcategoriaDTO> GetImplantologiaSubcategoria();
        public IEnumerable<SubcategoriaDTO> GetSubcategoriaById(int id);
    }
}
