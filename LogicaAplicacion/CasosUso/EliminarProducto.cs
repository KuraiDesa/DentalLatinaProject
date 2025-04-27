using LogicaAplicacion.InterfacesCasosUso;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class EliminarProducto : IEliminarProducto
    {
        public IRepositorioProducto repositorioProducto { get; set; }
        public EliminarProducto(IRepositorioProducto repositorioProducto)
        {
            this.repositorioProducto = repositorioProducto;
        }
        public void DelProd(int id)
        {
            try
            {
                repositorioProducto.Remove(id);
            }
            catch (Exception ex) 
            {
                throw new Exception("Algo salio mal");
            }
        }
    }
}
