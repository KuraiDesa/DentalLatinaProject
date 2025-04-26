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
    public class AltaSubcategoria:IAltaSubcategoria
    {
        public IRepositorioSubcategoria rep { get; set; }
        private readonly IRepositorioCategoria repositorioCategoria;

        public AltaSubcategoria(IRepositorioSubcategoria rep, IRepositorioCategoria repositorioCategoria)
        {
            this.rep = rep;
            this.repositorioCategoria = repositorioCategoria;
        }
        public void Alta(string cat, int i)
        {
            try
            {
                Categoria ca = repositorioCategoria.FindById(i);
                Subcategoria cate = new Subcategoria(cat, ca);
                rep.Add(cate);
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal");
            }

        }
    }
}
