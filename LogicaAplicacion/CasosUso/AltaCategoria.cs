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
    public class AltaCategoria:IAltaCategoria
    {
        public IRepositorioCategoria rep {  get; set; }

        public AltaCategoria(IRepositorioCategoria rep)
        {
            this.rep = rep;
        }   
        public void Alta(string cat)
        {
            try
            {
                Categoria cate = new Categoria(cat);
                rep.Add(cate);
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal");
            }
            
        }
    }
}
