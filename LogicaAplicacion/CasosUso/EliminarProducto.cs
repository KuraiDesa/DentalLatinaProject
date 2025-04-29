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
                // 1. Buscar el producto antes de eliminarlo
                var producto = repositorioProducto.FindById(id);

                if (producto == null)
                {
                    throw new Exception("Producto no encontrado");
                }

                // 2. Obtener la ruta de la imagen
                var rutaImagen = producto.photoUrl; // Asegúrate que este sea el campo correcto
                var rutaDocumento = producto.documentacion;
                // 3. Borrar la imagen si existe
                BorrarArchivoImagen(rutaImagen);
                BorrarArchivoDocumento(rutaDocumento);
                // 4. Eliminar el producto del repositorio
                repositorioProducto.Remove(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar producto: {ex.Message}", ex);
            }
        }
        private void BorrarArchivoImagen(string rutaImagenRelativa)
        {
            if (string.IsNullOrEmpty(rutaImagenRelativa))
                return;

            // Quitar "/" o "\" inicial si existe
            rutaImagenRelativa = rutaImagenRelativa.TrimStart('/', '\\');

            // Construir ruta absoluta
            string rutaFisica = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaImagenRelativa);

            // Verificar si existe y eliminar
            if (File.Exists(rutaFisica))
            {
                File.Delete(rutaFisica);
            }
        }
        private void BorrarArchivoDocumento(string rutaDocumentoRelativa)
        {
            if (string.IsNullOrEmpty(rutaDocumentoRelativa))
                return;

            // Quitar "/" o "\" inicial si existe
            rutaDocumentoRelativa = rutaDocumentoRelativa.TrimStart('/', '\\');

            // Construir ruta absoluta
            string rutaFisica = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaDocumentoRelativa);

            // Verificar si existe y eliminar
            if (File.Exists(rutaFisica))
            {
                File.Delete(rutaFisica);
            }
        }
    }
}

    

