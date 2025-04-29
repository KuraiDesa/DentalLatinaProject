using DentalLatina;
using DTOs.DTOs;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
using System.IO;

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
        public IAltaSubcategoria CUAltaSubcategoria { get; set; }
        public AdminController(IListarCategorias CUC, IlistarSubcategorias CUS, IAltaProducto altaProd, IListarClientes CUListarClientes,
                                IListarProductos CUListarProductos, IEliminarProducto CUEliminarProductro, IAltaCategoria AltaCategoria,
                                IAltaSubcategoria AltaSubcategoria)
        {
            this.CUListarCategorias = CUC;
            this.CUSubcategorias = CUS;
            this.altaProd = altaProd; 
            this.CUListarClientes = CUListarClientes;
            this.CUListarProductos = CUListarProductos;
            this.CUEliminarProductro = CUEliminarProductro;
            this.CUAltaCategoria = AltaCategoria;
            this.CUAltaSubcategoria = AltaSubcategoria;
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

                // Si el archivo ya existe, agregar un sufijo para evitar sobrescritura
                if (System.IO.File.Exists(rutaImagen))  // Uso explícito de System.IO.File
                {
                    var extension = Path.GetExtension(nombreImagen);
                    var nombreBase = Path.GetFileNameWithoutExtension(nombreImagen);
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    nombreImagen = $"{nombreBase}_{timestamp}{extension}";
                    rutaImagen = Path.Combine(carpetaImagenes, nombreImagen);
                }

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

                // Si el archivo ya existe, agregar un sufijo para evitar sobrescritura
                if (System.IO.File.Exists(rutaDoc))  // Uso explícito de System.IO.File
                {
                    var extension = Path.GetExtension(nombreDoc);
                    var nombreBase = Path.GetFileNameWithoutExtension(nombreDoc);
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    nombreDoc = $"{nombreBase}_{timestamp}{extension}";
                    rutaDoc = Path.Combine(carpetaDocs, nombreDoc);
                }

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

        [HttpPost]
        [ActionName("CreateCategoria")]
        public IActionResult CreateCategoria(GeneralViewModel generalViewModel)
        {
            VerificarSesion(); // Por seguridad

            if (string.IsNullOrWhiteSpace(generalViewModel.categoria.nombre))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            try
            {
                CUAltaCategoria.Alta(generalViewModel.categoria.nombre);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al crear categoría: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ActionName("CreateSubcategoria")]
        public IActionResult CreateSubcategoria(GeneralViewModel generalViewModel)
        {
            VerificarSesion(); // Por seguridad

            if (string.IsNullOrWhiteSpace(generalViewModel.subcategoria.nombre))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            try
            {
                CUAltaSubcategoria.Alta(
                    generalViewModel.subcategoria.nombre,
                    generalViewModel.subcategoria.idcat
                    );
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al crear categoría: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult GetSubcategoriasPorCategoria(int categoriaId)
        {
            // Obtén todas las subcategorías
            var todas = CUSubcategorias.GetSubcategoriaById(categoriaId);

            // Filtra por la propiedad correcta (aquí asumo que la entidad tiene un campo CategoriaId)
            var subcategorias = todas
                .Select(s => new {
                    id = s.id,      // propón nombres sencillos: id y nombre
                    nombre = s.nombre
                })
                .ToList();

            // Siempre devuelve un JSON de lista, aunque esté vacío
            return Json(subcategorias);
        }
    }

    


    public class GeneralViewModel
    {
        public ProductoCategoriaViewModel productocategoriaview { get; set; }
        public ProductoViewModel productoview { get; set; }
        public CategoriaViewModel categoria { get; set; }
        public SubcategoriaViewModel subcategoria { get; set; }
        public IEnumerable<ClienteDTO> clientes { get; set; }
        
        public IEnumerable<ProductoDTO> productos { get; set; }
    }
    public class ProductoCategoriaViewModel
    {
        public IEnumerable<SubcategoriaDTO> Subcategoria { get; set; }
        public IEnumerable<CategoriaDTO> Categorias { get; set; }
    }

    public class CategoriaViewModel
    {
        public string nombre { get; set; }
    }

    public class SubcategoriaViewModel
    {
        public string nombre { get; set; }
        public int idcat {  get; set; }
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
