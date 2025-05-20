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
    public class EliminarCategoriaSub : IEliminarCategoriaSub
    {
        public IRepositorioSubcategoria repS { get; set; }
        public IRepositorioCategoria repC { get; set; }
        public IRepositorioProducto repP { get; set; }
        public IRepositorioCEspecial repCE {  get; set; }
        public EliminarCategoriaSub(IRepositorioCategoria repositorioCat, IRepositorioSubcategoria repositorioSub, IRepositorioProducto repositorioProd, IRepositorioCEspecial repCE)
        {
            this.repS = repositorioSub;
            this.repC = repositorioCat;
            this.repP = repositorioProd;
            this.repCE = repCE;
        }
        public void eliminarCategoria(int id)
        {
            repP.RemoveByCatId(id);
            repS.RemoveByCatId(id);
            repC.Remove(id);
        }

        public void eliminarSubcategoria(int id)
        {
            repP.RemoveByScatId(id);
            repS.Remove(id);
        }

        public void eliminarCategoriaEspecial(int id)
        {
            repP.RemoveByCEId(id);
            repCE.Remove(id);
        }
    }
}
