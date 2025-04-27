using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class ListarSubcategorias : IlistarSubcategorias
    {
        public IRepositorioSubcategoria repoSubcategoria { get; set; }
        public ListarSubcategorias(IRepositorioSubcategoria repo)
        {
            repoSubcategoria = repo;
        }
        public IEnumerable<SubcategoriaDTO> GetSubcategoria()
        {
            try
            {
                IEnumerable<Subcategoria> ListaSubcategoria = repoSubcategoria.FindAll();

                IEnumerable<SubcategoriaDTO> listaSubcategoriaDTO = SubategoriaMapper.ToListaSubcategoriaDTO(ListaSubcategoria);

                return listaSubcategoriaDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer categoria");
            }
        }

        public IEnumerable<SubcategoriaDTO> GetSubcategoriaById(int id)
        {
            try
            {
                IEnumerable<Subcategoria> ListaSubcategoria = repoSubcategoria.FindByIdList(id);

                IEnumerable<SubcategoriaDTO> listaSubcategoriaDTO = SubategoriaMapper.ToListaSubcategoriaDTO(ListaSubcategoria);

                return listaSubcategoriaDTO;
            } catch (Exception ex)
            {
                throw new Exception("Error al traer categoria");
            }
        }
    }
}
