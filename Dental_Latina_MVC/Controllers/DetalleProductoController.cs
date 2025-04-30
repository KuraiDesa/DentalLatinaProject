using DTOs.DTOs;
using Humanizer;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dental_Latina_MVC.Controllers
{
    public class DetalleProductoController : Controller
    {
        IDetalleProducto CUDetalleProducto { get; set; }
        public DetalleProductoController(IDetalleProducto detalleProducto)
        {
            this.CUDetalleProducto = detalleProducto;
        }
        // GET: DetalleProductoController
        public ActionResult Index(int id)
        {
            try
            {
                ProductoDTO dto = CUDetalleProducto.detalleProducto(id);
                return View(dto);
            }
            catch (Exception ex)
            {
                return View();
            }
            
            
        }

        // GET: DetalleProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetalleProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetalleProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DetalleProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DetalleProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
