using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Mvc;

namespace Dental_Latina_MVC.Controllers
{
    public class ProductoController : Controller
    {
        public IListarProductos CUListarProductos { get; set; }
        public ProductoController(IListarProductos CU)
        {
            this.CUListarProductos = CU;
        }
        public IActionResult Index()
        {

            try
            {
                IEnumerable<ProductoDTO> listaProductos = CUListarProductos.GetProductos();

                return View(listaProductos);
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }

        }
    }
}
