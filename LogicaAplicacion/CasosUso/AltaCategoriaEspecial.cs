using LogicaAplicacion.InterfacesCasosUso;
using LogicaDatos.Repositorios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class AltaCategoriaEspecial : IAltaCategoriaEspecial
    {
        public IRepositorioSubcategoria repoSC { get; set; }
        public IRepositorioCEspecial repo {  get; set; }
        public AltaCategoriaEspecial(IRepositorioCEspecial repositorioCEspecial, IRepositorioSubcategoria repoSC)
        {
            this.repo = repositorioCEspecial;
            this.repoSC = repoSC;
        }
        public void Alta(string nombre, int id)
        {
            try
            {
                Subcategoria subc = repoSC.FindById(id);
                CEspecial ces = new CEspecial(nombre);
                ces.agregarSub(subc);
                repo.Add(ces);
            }
            catch(Exception ex)
            {
                throw new Exception("Algo salio mal");
            }
        }
    }
}
