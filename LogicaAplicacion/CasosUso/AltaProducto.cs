using DentalLatina;
using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaDatos.EntityFramework;
using LogicaDatos.Repositorios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaAplicacion.CasosUso
{
    public class AltaProducto : IAltaProducto
    {
        private readonly IRepositorioProducto repoProd;
        private readonly IRepositorioCategoria repositorioCategoria;
        private readonly IRepositorioSubcategoria repositorioSubcategoria;
        private readonly IRepositorioCEspecial repositorioCEspecial;
        private readonly LibreriaContext context;

        public AltaProducto(IRepositorioProducto repo, IRepositorioCategoria repositorioCategoria, IRepositorioSubcategoria repositorioSubcategoria, IRepositorioCEspecial repositorioCEspecial, LibreriaContext context)
        {
            this.repoProd = repo;
            this.context = context;
            this.repositorioCategoria= repositorioCategoria;
            this.repositorioSubcategoria= repositorioSubcategoria;
            this.repositorioCEspecial= repositorioCEspecial;
        }

        public void AltaProd(string nombre, string url, string descripcion, int categoria, int subcategoria, string documentacion, int precio)
        {
            Categoria cate = repositorioCategoria.FindById(categoria);
            Subcategoria scate = repositorioSubcategoria.FindById(subcategoria);
            Producto prod = new Producto(nombre, url, cate, scate, documentacion, descripcion, precio);
            repoProd.Add(prod);
        }

        public void AltaProdCatEspecial(string nombre, string url, string descripcion, int categoria, int subcategoria, int categroiaEspecial, string documentacion, int precio)
        {
            CEspecial catEsp = repositorioCEspecial.FindById(categroiaEspecial);
            Categoria cate = repositorioCategoria.FindById(categoria);
            Subcategoria scate = repositorioSubcategoria.FindById(subcategoria);
            Producto prod = new Producto(nombre, url, cate, scate, documentacion, descripcion, precio);
            prod.agregarCategoriaEspecial(catEsp);
            repoProd.Add(prod);
        }

        public bool verificarCategoria(int categoria)
        {
            Categoria cate = repositorioCategoria.FindById(categoria);
            if (cate.Nombre == "Implantologia")
            {
                return true;
            }
            return false;
        }
    }
}
