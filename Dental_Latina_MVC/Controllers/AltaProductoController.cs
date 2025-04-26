using DTOs.DTOs;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Mvc;

namespace Dental_Latina_MVC.Controllers
{
    public class AltaProductoController : Controller
    {
        /*public IListarCategorias CUListarCategorias { get; set; }
        public IlistarSubcategorias CUSubcategorias { get; set; }
        public AltaProductoController(IListarCategorias CUC, IlistarSubcategorias CUS)
        {
            this.CUListarCategorias = CUC;
            this.CUSubcategorias = CUS;
        }
        public IActionResult Index()
        {
            try
            {

                IEnumerable<CategoriaDTO> listaCategoria = CUListarCategorias.GetCategoria();
                IEnumerable<SubcategoriaDTO> listaSubcategoria = CUSubcategorias.GetSubcategoria();

                ProductoCategoriaViewModel viewModel = new ProductoCategoriaViewModel
                {
                    Subcategoria = listaSubcategoria,
                    Categorias = listaCategoria
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }*/
    }
    /*public class ProductoCategoriaViewModel
    {
        public IEnumerable<SubcategoriaDTO> Subcategoria { get; set; }
        public IEnumerable<CategoriaDTO> Categorias { get; set; }
    }*/
}
