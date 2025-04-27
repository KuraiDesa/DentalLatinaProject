using DentalLatina;
using DTOs.DTOs;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dental_Latina_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IListarCategorias CUListarCategorias { get; set; }
        public IlistarSubcategorias CUSubcategorias { get; set; }
        public IListarProductos CUListarProductos { get; set; }
        public IEliminarProducto CUEliminarProductro { get; set; }
        public IAltaProducto altaProd { get; set; }
        public IListarClientes CUListarClientes { get; set; }
        public IAltaCategoria CUAltaCategoria { get; set; }
        public AdminController(IListarCategorias CUC, IlistarSubcategorias CUS, IAltaProducto altaProd, IListarClientes CUListarClientes, IListarProductos CUListarProductos, IEliminarProducto CUEliminarProductro, IAltaCategoria AltaCategoria)
        {
            this.CUListarCategorias = CUC;
            this.CUSubcategorias = CUS;
            this.altaProd = altaProd; 
            this.CUListarClientes = CUListarClientes;
            this.CUListarProductos = CUListarProductos;
            this.CUEliminarProductro = CUEliminarProductro;
            this.CUAltaCategoria = AltaCategoria;
        }
        
        public IActionResult VerificarSesion()
        {
            // Verificar si la sesión contiene un usuario autenticado
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                // Si no hay sesión activa, redirigir al login
                return RedirectToAction("Index", "Home");
            }

            // Si la sesión es válida, continuar con la ejecución normal
            return null; // Retorna null si no necesitas redirigir
        }
        public IActionResult Index()
        {

            VerificarSesion();
            try
            {
                
                IEnumerable<CategoriaDTO> listaCategoria = CUListarCategorias.GetCategoria();
                IEnumerable<SubcategoriaDTO> listaSubcategoria = CUSubcategorias.GetSubcategoria();
                IEnumerable<ClienteDTO> clientesDTO = CUListarClientes.GetClientes();
                IEnumerable<ProductoDTO> productosDTO = CUListarProductos.GetProductos();
                ProductoCategoriaViewModel prodviewModel = new ProductoCategoriaViewModel
                {
                    Subcategoria = listaSubcategoria,
                    Categorias = listaCategoria
                };

                GeneralViewModel viewModel = new GeneralViewModel
                {
                    productocategoriaview = prodviewModel,
                    productos = productosDTO,
                    clientes = clientesDTO
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create(GeneralViewModel generalViewModel)
        {
            VerificarSesion();

            // Preparar carpetas
            var wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var carpetaImagenes = Path.Combine(wwwroot, "imagenes", "productos");
            var carpetaDocs = Path.Combine(wwwroot, "documentos");

            Directory.CreateDirectory(carpetaImagenes);
            Directory.CreateDirectory(carpetaDocs);

            // Guardar imagen si se cargó
            if (generalViewModel.productoview.ImagenArchivo != null && generalViewModel.productoview.ImagenArchivo.Length > 0)
            {
                var nombreImagen = Path.GetFileName(generalViewModel.productoview.ImagenArchivo.FileName);
                var rutaImagen = Path.Combine(carpetaImagenes, nombreImagen);
                using (var stream = new FileStream(rutaImagen, FileMode.Create))
                {
                    await generalViewModel.productoview.ImagenArchivo.CopyToAsync(stream);
                }
                generalViewModel.productoview.PhotoUrl = "/imagenes/productos/" + nombreImagen;
            }

            // Guardar documentación si se cargó
            if (generalViewModel.productoview.DocumentacionArchivo != null && generalViewModel.productoview.DocumentacionArchivo.Length > 0)
            {
                var nombreDoc = Path.GetFileName(generalViewModel.productoview.DocumentacionArchivo.FileName);
                var rutaDoc = Path.Combine(carpetaDocs, nombreDoc);
                using (var stream = new FileStream(rutaDoc, FileMode.Create))
                {
                    await generalViewModel.productoview.DocumentacionArchivo.CopyToAsync(stream);
                }
                generalViewModel.productoview.DocumentacionUrl = "/documentos/" + nombreDoc;
            }
            else
            {
                generalViewModel.productoview.DocumentacionUrl = "Sin Documentacion";
            }

            // Guardar en tu lógica
            altaProd.AltaProd(
                generalViewModel.productoview.Nombre,
                generalViewModel.productoview.PhotoUrl,
                generalViewModel.productoview.Descripcion,
                generalViewModel.productoview.CategoriaId,
                generalViewModel.productoview.SubcategoriaId,
                generalViewModel.productoview.DocumentacionUrl,
                generalViewModel.productoview.Precio ?? 0
            );

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            try
            {
                CUEliminarProductro.DelProd(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }


    }



    public class GeneralViewModel
    {
        public ProductoCategoriaViewModel productocategoriaview { get; set; }
        public ProductoViewModel productoview { get; set; }
        public IEnumerable<ClienteDTO> clientes { get; set; }
        
        public IEnumerable<ProductoDTO> productos { get; set; }
    }
    public class ProductoCategoriaViewModel
    {
        public IEnumerable<SubcategoriaDTO> Subcategoria { get; set; }
        public IEnumerable<CategoriaDTO> Categorias { get; set; }
    }
    public class ProductoViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        public string? PhotoUrl { get; set; } // lo vamos a llenar nosotros después

        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public int SubcategoriaId { get; set; }

        public string? DocumentacionUrl { get; set; } // opcional, para guardar la ruta
        public int? Precio { get; set; }

        public IFormFile ImagenArchivo { get; set; } // ⬅️ Imagen subida
        public IFormFile DocumentacionArchivo { get; set; } // ⬅️ Documento opcional
    }

}
