using DentalLatina;
using DTOs.DTOs;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Dental_Latina_MVC.Controllers
{
    public class ProductoController : Controller
    {
        public  IListarProductos CUListarProductos { get; set; }
        public  IListarCategorias CUCategorias { get; set; }
        public IlistarSubcategorias CUSubcategorias { get; set; }
        public IListarCategoriasEspeciales CUCategoriasEspeciales { get; set; }
 
        public ProductoController(IListarProductos CU, IListarCategorias CatCU, IlistarSubcategorias cUSubcategorias, IListarCategoriasEspeciales cUCategoriasEspeciales)
        {
            this.CUListarProductos = CU;
            this.CUCategorias = CatCU;
            CUSubcategorias = cUSubcategorias;
            CUCategoriasEspeciales = cUCategoriasEspeciales;
        }

        public IActionResult Index(int page = 1, int filtrando = 0, Boolean byName = false, string nombre = "", int tipo =-1, int cat=-1)
        {
            

            IEnumerable<ProductoDTO> listaProductos= CUListarProductos.GetProductos();
            if (!Request.Query.Any())
            {
                return RedirectToAction(nameof(Index), new { page = 1, filtrando = 0, nombre = "" });
            }

            if (tipo == 0)
            {
                listaProductos = CUListarProductos.ListarProductosCategoria(cat);

            }
            else if (tipo == 1)
            {
                listaProductos = CUListarProductos.ListarProductosSubcategoria(cat);

            }
            else if (tipo == 2)
            {
                listaProductos = CUListarProductos.ListarProductosCategoriaEspecial(cat);

            }
            if (filtrando!=0&&byName==false) {
                listaProductos = CUListarProductos.ListarProductosCategoria(filtrando);                    
            }
            if (byName==true&&filtrando==0)
            {
                listaProductos = CUListarProductos.ListarProductosNombre(nombre);
            }
            if (byName==true&&filtrando!=0)
            {
                listaProductos = CUListarProductos.ListarPorCateNombre(filtrando, nombre);
            }
            IEnumerable<CategoriaDTO> listarCategorias = CUCategorias.GetCategoria();
            GeneralProductosViewModel general = new GeneralProductosViewModel
            {
                Productos = listaProductos,
                Categorias = listarCategorias,
                Subcategorias = CUSubcategorias.GetSubcategoria(),
                CategoriasEspeciales = CUCategoriasEspeciales.GetCategoriaespecial(),
                cantPaginas = (int)Math.Ceiling((double)listaProductos.Count() / 9),
                paginaActual = page,
                paginador = page - 1,
                catFilter=filtrando,
                searchByName=nombre,
                tipo=tipo,
                cat=cat
            };

            return View(general);

        }
        [HttpPost]
        public IActionResult Filter(GeneralProductosViewModel productoModel)
        {
            if (productoModel.catFilter == 0&&productoModel.searchByName==null)
            {
                return RedirectToAction("Index", new { page = 1, filtrando = 0, byName =false});
            }
            else if(productoModel.catFilter != 0 && productoModel.searchByName == null)
            {
                return RedirectToAction("Index", new { page=1, filtrando = productoModel.catFilter, byName = false});
            }else if(productoModel.catFilter == 0 && productoModel.searchByName != "")
            {
                return RedirectToAction("Index", new { page = 1, filtrando = 0, byName = true, nombre = productoModel.searchByName });
            }else if(productoModel.catFilter != 0 && productoModel.searchByName != "")
            {
                return RedirectToAction("Index", new { page = 1, filtrando = productoModel.catFilter, byName = true, nombre = productoModel.searchByName });
            }
            else
            {
                return RedirectToAction("Index");
            }
        
        }

        [HttpGet]
        public IActionResult FilterAjax(int id, int tipo)
        {
            return RedirectToAction("Index", new { page = 1, tipo = tipo, cat = id });
        }

        [HttpGet]
        public IActionResult FilterSubcategorias(int cateid)
        {
            var lista = CUSubcategorias.GetSubcategoriaById(cateid);
            var subcates = lista
                .Select(s => new {
                    id = s.id,
                    nombre = s.nombre
                })
                .ToList();
            return Json(subcates);
        }
        [HttpGet]
        public IActionResult FilterCategoriaEspecial(int cateid)
        {
            var lista = CUCategoriasEspeciales.GetCategoriaEspecialById(cateid);
            var subcates = lista
                .Select(s => new {
                    id = s.id,
                    nombre = s.nombre
                })
                .ToList();
            return Json(subcates);
        }
    }
    
    

    public class GeneralProductosViewModel
    {
        public IEnumerable<ProductoDTO> Productos { get; set; }
        public IEnumerable<CategoriaDTO> Categorias { get; set; }
        public IEnumerable<SubcategoriaDTO> Subcategorias { get; set; }
        public IEnumerable<CategoriaEspecialDTO> CategoriasEspeciales { get; set; }
        public int cantPaginas {  get; set; }
        public int paginaActual {  get; set; }
        public int catFilter {  get; set; }
        public int subFilter { get; set; }
        public int cat {  get; set; }
        public int tipo { get; set; }
        public string searchByName { get; set; }
        public int paginador { get; set; }
        public Boolean searchingByName {  get; set; }
    }
}
