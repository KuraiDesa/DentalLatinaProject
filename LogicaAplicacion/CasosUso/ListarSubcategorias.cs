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
        public IRepositorioCategoria repoCategoria { get; set; }
        public ListarSubcategorias(IRepositorioSubcategoria repo, IRepositorioCategoria repoCategoria)
        {
            repoSubcategoria = repo;
            this.repoCategoria = repoCategoria;
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

        public IEnumerable<SubcategoriaDTO> GetImplantologiaSubcategoria()
        {
            int idImplanto = repoCategoria.FindIdByNombre("Implantologia");
            IEnumerable<SubcategoriaDTO> lista = SubategoriaMapper.ToListaSubcategoriaDTO(repoSubcategoria.FindByIdList(idImplanto));
            return lista;
        }
    }
}
