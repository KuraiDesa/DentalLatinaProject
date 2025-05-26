using DentalLatina;
using DTOs.DTOs;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace Dental_Latina_MVC.Controllers
{
    

    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IListarCategorias CUListarCategorias { get; set; }
        public IlistarSubcategorias CUSubcategorias { get; set; }
        public IListarProductos CUListarProductos { get; set; }
        public IEliminarProducto CUEliminarProductro { get; set; }
        public IAltaProducto CUAltaProd { get; set; }
        public IListarClientes CUListarClientes { get; set; }
        public IAltaCategoria CUAltaCategoria { get; set; }
        public IAltaSubcategoria CUAltaSubcategoria { get; set; }
        public IAltaCategoriaEspecial CUAltaCategoriaEspecial { get; set; }
        public IEliminarCategoriaSub CUEliminarCategoriaSub { get; set; }
        public IListarCategoriasEspeciales CUListarCategoriaEspecial {  get; set; }
        public IDetalleZona CUZONAS { get; set; }
        public AdminController(IListarCategorias CUC, IlistarSubcategorias CUS, IAltaProducto altaProd, IListarClientes CUListarClientes,
                                IListarProductos CUListarProductos, IEliminarProducto CUEliminarProductro, IAltaCategoria AltaCategoria,
                                IAltaSubcategoria AltaSubcategoria, IEliminarCategoriaSub eliminarCategoriaSub, IListarCategoriasEspeciales CUCS,
                                IAltaCategoriaEspecial AltaCategoriaEspecial, IDetalleZona d)
        {
            this.CUListarCategorias = CUC;
            this.CUSubcategorias = CUS;
            this.CUListarCategoriaEspecial = CUCS;
            this.CUAltaProd = altaProd; 
            this.CUListarClientes = CUListarClientes;
            this.CUListarProductos = CUListarProductos;
            this.CUEliminarProductro = CUEliminarProductro;
            this.CUAltaCategoria = AltaCategoria;
            this.CUAltaSubcategoria = AltaSubcategoria;
            this.CUEliminarCategoriaSub = eliminarCategoriaSub;
            this.CUAltaCategoriaEspecial = AltaCategoriaEspecial;
            this.CUZONAS = d;
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
                IEnumerable<SubcategoriaDTO> listaSubcategoriaImplantologia = CUSubcategorias.GetImplantologiaSubcategoria();
                IEnumerable<CategoriaEspecialDTO> listaCategoriaEspeciales = CUListarCategoriaEspecial.GetCategoriaespecial();
                IEnumerable<ClienteDTO> clientesDTO = CUListarClientes.GetClientes();
                IEnumerable<ProductoDTO> productosDTO = CUListarProductos.GetProductos();
                IEnumerable<ZonaDTO> zonasDTO = CUZONAS.getZonas();
                ProductoCategoriaViewModel prodviewModel = new ProductoCategoriaViewModel
                {
                    Subcategoria = listaSubcategoria,
                    Categorias = listaCategoria,
                    CategoriasEsepciales = listaCategoriaEspeciales,
                    SubcateImplantologia = listaSubcategoriaImplantologia
                };

                GeneralViewModel viewModel = new GeneralViewModel
                {
                    productocategoriaview = prodviewModel,
                    productos = productosDTO,
                    clientes = clientesDTO,
                    zonas = zonasDTO
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

            if (CUAltaProd.verificarCategoria(generalViewModel.productoview.CategoriaId))
            {
                CUAltaProd.AltaProdCatEspecial(
                generalViewModel.productoview.Nombre,
                generalViewModel.productoview.PhotoUrl,
                generalViewModel.productoview.Descripcion,
                generalViewModel.productoview.CategoriaId,
                generalViewModel.productoview.SubcategoriaId,
                generalViewModel.productoview.CategoriaEspecial,
                generalViewModel.productoview.DocumentacionUrl,
                generalViewModel.productoview.Precio ?? 0
                );
            }
            else {
                CUAltaProd.AltaProd(
                generalViewModel.productoview.Nombre,
                generalViewModel.productoview.PhotoUrl,
                generalViewModel.productoview.Descripcion,
                generalViewModel.productoview.CategoriaId,
                generalViewModel.productoview.SubcategoriaId,
                generalViewModel.productoview.DocumentacionUrl,
                generalViewModel.productoview.Precio ?? 0
                );
            }
            

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
            VerificarSesion();

            if ((generalViewModel.categoria.nombre).Equals(""))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            try
            {
                CUAltaCategoria.Alta(
                    generalViewModel.categoria.nombre
                    );
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

        [HttpPost]
        [ActionName("CreateCateEspecial")]
        public IActionResult CreateCateEspecial(GeneralViewModel generalViewModel)
        {
            VerificarSesion();


            if (string.IsNullOrWhiteSpace(generalViewModel.categoriaEspecial.nombre))
            {
                return BadRequest("El nombre de la categoría especial es obligatorio.");
            }

            try
            {
                CUAltaCategoriaEspecial.Alta(
                    generalViewModel.categoriaEspecial.nombre,
                    generalViewModel.categoriaEspecial.idscat
                    );
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al crear categoría: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
       

        [HttpPost]
        [ActionName("RemoveCategoria")]
        public IActionResult removeCategoria(GeneralViewModel generalViewModel)
        {
            VerificarSesion(); // Por seguridad

            if (generalViewModel.categoria.id <= 0)
            {
                return BadRequest("Eliga una categoria valida.");
            }

            try
            {
                CUEliminarCategoriaSub.eliminarCategoria(generalViewModel.categoria.id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar categoria: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ActionName("RemoveSubcategoria")]
        public IActionResult removeSubcategoria(GeneralViewModel generalViewModel)
        {
            VerificarSesion(); // Por seguridad

            if (generalViewModel.subcategoria.id <= 0)
            {
                return BadRequest("Eliga una subcategoria valida.");
            }

            try
            {
                CUEliminarCategoriaSub.eliminarSubcategoria(generalViewModel.subcategoria.id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar subcategoria: " + ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ActionName("RemoveCategoriaEspecial")]
        public IActionResult removeCategoriaEspecial(GeneralViewModel generalViewModel)
        {
            VerificarSesion(); // Por seguridad

            if (generalViewModel.productoview.CategoriaEspecial <= 0)
            {
                return BadRequest("Eliga una categoria especial valida.");
            }

            try
            {
                CUEliminarCategoriaSub.eliminarCategoriaEspecial(generalViewModel.productoview.CategoriaEspecial);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar subcategoria: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult GetSubcategoriasPorCategoria(int categoriaId)
        {

            var todas = CUSubcategorias.GetSubcategoriaById(categoriaId);
            var subcategorias = todas
                .Select(s => new {
                    id = s.id,
                    nombre = s.nombre
                })
                .ToList();

            return Json(subcategorias);
        }

        [HttpGet]
        public IActionResult GetCategoriaEspecialPorSubcategoria(int subcategoriaId)
        {

            var todas = CUListarCategoriaEspecial.GetCategoriaEspecialById(subcategoriaId);
            var categoriaespecial = todas
                .Select(s => new {
                    id = s.id,
                    nombre = s.nombre
                })
                .ToList();

            return Json(categoriaespecial);
        }

        [HttpGet]
        public IActionResult FilterByCategoria(int categoria, string nombre)
        {
            IEnumerable<ProductoDTO> lista=[];
            if (string.IsNullOrEmpty(nombre) && categoria != -1)
            {
                lista = CUListarProductos.ListarProductosCategoria(categoria);              
            }
            else if (string.IsNullOrEmpty(nombre) && categoria == -1)
            {
                lista = CUListarProductos.GetProductos();
            }
            else if (!string.IsNullOrEmpty(nombre) && categoria == -1)
            {
                lista = CUListarProductos.ListarProductosNombre(nombre);                
            }
            else if (!string.IsNullOrEmpty(nombre) && categoria != -1)
            {
                lista = CUListarProductos.ListarPorCateNombre(categoria, nombre);             
            }

            var productos = lista
                .Select(s => new {
                    id = s.id,
                    nombre = s.nombre,
                    photoUrl = s.photoUrl,
                    precio = s.precio,

                })
                .ToList();
            return Json(productos);
        }

        [HttpGet]
        public IActionResult GetAllCategorias()
        {
            var categorias = CUListarCategorias.GetCategoria()
                .Select(s=> new
                {
                    id = s.id,
                    nombre= s.nombre,
                }).ToList();    
            return Json(categorias);
        }


        [HttpPost]
        public IActionResult GuardarZona([FromBody] ZonaDTO zona)
        {
    

            return Json(new { exito = true, mensaje = "Zona guardada" });
        }


        [HttpGet]
        public IActionResult ProductoModify(int id)
        {
            var producto = CUListarProductos.GetProducto(id);
            return Json(producto);
        }
        [HttpPost]
        public async Task<IActionResult> ModifyProducto(GeneralViewModel generalViewModel)
        {
            VerificarSesion();

            // Preparar carpetas
            var wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var carpetaImagenes = Path.Combine(wwwroot, "imagenes", "productos");
            var carpetaDocs = Path.Combine(wwwroot, "documentos");

            Directory.CreateDirectory(carpetaImagenes);
            Directory.CreateDirectory(carpetaDocs);

            // Guardar imagen si se cargó
            if (generalViewModel.modifyproducto.ImagenArchivo != null && generalViewModel.modifyproducto.ImagenArchivo.Length > 0)
            {
                var nombreImagen = Path.GetFileName(generalViewModel.modifyproducto.ImagenArchivo.FileName);
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
                    await generalViewModel.modifyproducto.ImagenArchivo.CopyToAsync(stream);
                }

                generalViewModel.modifyproducto.PhotoUrl = "/imagenes/productos/" + nombreImagen;
            }

            // Guardar documentación si se cargó
            if (generalViewModel.modifyproducto.DocumentacionArchivo != null && generalViewModel.modifyproducto.DocumentacionArchivo.Length > 0)
            {
                var nombreDoc = Path.GetFileName(generalViewModel.modifyproducto.DocumentacionArchivo.FileName);
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
                    await generalViewModel.modifyproducto.DocumentacionArchivo.CopyToAsync(stream);
                }

                generalViewModel.modifyproducto.DocumentacionUrl = "/documentos/" + nombreDoc;
            }
            else if(generalViewModel.modifyproducto.DocumentacionUrl.Equals(""))
            {
                generalViewModel.modifyproducto.DocumentacionUrl = "Sin Documentacion";
            }


                CUAltaProd.ModifyProd(
                generalViewModel.modifyproducto.id,
                generalViewModel.modifyproducto.Nombre,
                generalViewModel.modifyproducto.PhotoUrl,
                generalViewModel.modifyproducto.Descripcion,
                generalViewModel.modifyproducto.CategoriaId,
                generalViewModel.modifyproducto.SubcategoriaId,
                generalViewModel.modifyproducto.CategoriaEspecial ?? 0,
                generalViewModel.modifyproducto.DocumentacionUrl,
                generalViewModel.modifyproducto.Precio ?? 0
                );



            return RedirectToAction("Index");
        }

        public async Task<IActionResult> salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");

        }
    }

 


    public class GeneralViewModel
    {
        public ProductoCategoriaViewModel productocategoriaview { get; set; }
        public ProductoViewModel productoview { get; set; }
        public ModifyProductoViewModel modifyproducto { get; set; }
        public CategoriaViewModel categoria { get; set; }
        public SubcategoriaViewModel subcategoria { get; set; }
        public CategoriaEspecialViewModel categoriaEspecial { get; set; }
        public IEnumerable<ClienteDTO> clientes { get; set; }      
        public IEnumerable<ProductoDTO> productos { get; set; }
        public IEnumerable<ZonaDTO> zonas { get; set; }
        public ProductoDTO productoModificar {  get; set; }
    }
    public class ProductoCategoriaViewModel
    {
        public IEnumerable<SubcategoriaDTO> Subcategoria { get; set; }
        public IEnumerable<SubcategoriaDTO> SubcateImplantologia { get; set; }
        public IEnumerable<CategoriaDTO> Categorias { get; set; }
        public IEnumerable<CategoriaEspecialDTO> CategoriasEsepciales { get; set; }
    }

    public class CategoriaViewModel
    {
        public string nombre { get; set; }
        public int id { get; set; }
    }

    public class SubcategoriaViewModel
    {
        public string nombre { get; set; }
        public int idcat { get; set; }
        public int id { get; set; }
    }
    public class CategoriaEspecialViewModel
    {
        public string nombre { get; set; }
        public int id { get; set; }
        public int idscat { get; set; }
    }
    public class ModifyProductoViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public int id { get; set; }
        public string Nombre { get; set; }
        public string? PhotoUrl { get; set; } // lo vamos a llenar nosotros después
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public int SubcategoriaId { get; set; }
        public int? CategoriaEspecial { get; set; }
        public string? DocumentacionUrl { get; set; } // opcional, para guardar la ruta
        public int? Precio { get; set; }

        public IFormFile ImagenArchivo { get; set; } // ⬅️ Imagen subida
        public IFormFile DocumentacionArchivo { get; set; } // ⬅️ Documento opcional
    }
    public class ProductoViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        public string? PhotoUrl { get; set; } // lo vamos a llenar nosotros después

        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public int SubcategoriaId { get; set; }
        public int CategoriaEspecial { get; set; }
        public string? DocumentacionUrl { get; set; } // opcional, para guardar la ruta
        public int? Precio { get; set; }

        public IFormFile ImagenArchivo { get; set; } // ⬅️ Imagen subida
        public IFormFile DocumentacionArchivo { get; set; } // ⬅️ Documento opcional
    }

}
