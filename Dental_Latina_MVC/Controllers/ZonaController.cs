using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dental_Latina_MVC.Controllers
{
    public class ZonaController : Controller
    {
        private readonly IDetalleZona _cuDetalleZona;

        public ZonaController(IDetalleZona cuDetalleZona)
        {
            _cuDetalleZona = cuDetalleZona;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                ZonaDTO zona = _cuDetalleZona.ZonabyId(id);

                if (zona == null)
                {
                    return NotFound(new { error = "Zona no encontrada." });
                }

                return Ok(zona); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    error = "Error interno del servidor.",
                    detalle = ex.Message
                });
            }
        }
    }
}
