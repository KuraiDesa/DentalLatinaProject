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
        public EliminarCategoriaSub(IRepositorioCategoria repositorioCat, IRepositorioSubcategoria repositorioSub, IRepositorioProducto repositorioProd)
        {
            this.repS = repositorioSub;
            this.repC = repositorioCat;
            this.repP = repositorioProd;
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
    }
}
