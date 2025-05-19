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
    public class ListarCategoriasEspeciales : IListarCategoriasEspeciales
    {
        public IRepositorioCEspecial repositorioCespecial { get; set; }
        public ListarCategoriasEspeciales(IRepositorioCEspecial repo)
        {
            repositorioCespecial = repo;
        }

        public IEnumerable<CategoriaEspecialDTO> GetCategoriaespecial()
        {
            try
            {
                IEnumerable<CEspecial> ListaSubcategoria = repositorioCespecial.FindAll();

                IEnumerable<CategoriaEspecialDTO> listaSubcategoriaDTO = CategoriaEspecialMapper.ToListaCategoriaEspecialDTO(ListaSubcategoria);

                return listaSubcategoriaDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer categoria");
            }
        }

        public IEnumerable<CategoriaEspecialDTO> GetCategoriaEspecialById(int id)
        {
            try
            {
                IEnumerable<CEspecial> ListaCategoriaEspecial = repositorioCespecial.FindAllById(id);

                IEnumerable<CategoriaEspecialDTO> listaCategoriaEspecialDTO = CategoriaEspecialMapper.ToListaCategoriaEspecialDTO(ListaCategoriaEspecial);

                return listaCategoriaEspecialDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer categoria");
            }
        }
    }
}
