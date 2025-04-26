using DentalLatina;
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
    public class ListarCategorias : IListarCategorias
    {
        public IRepositorioCategoria repoCategoria { get; set; }
        public ListarCategorias(IRepositorioCategoria repo)
        {
            repoCategoria = repo;
        }
        public IEnumerable<CategoriaDTO> GetCategoria()
        {
            try
            {
                IEnumerable<Categoria> ListaCategoria = repoCategoria.FindAll();

                IEnumerable<CategoriaDTO> listaCategoriaDTO = CategoriaMapper.ToListaCategoriaDTO(ListaCategoria);

                return listaCategoriaDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer categoria");
            }
        }
    }
}
