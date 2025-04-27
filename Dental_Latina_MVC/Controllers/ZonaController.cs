using LogicaAplicacion.InterfacesCasosUso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dental_Latina_MVC.Controllers
{
    public class ZonaController : Controller
    {

        IDetalleZona CUDetalleZona {  get; set; }


        public ZonaController(IDetalleZona cu) 
        {
            this.CUDetalleZona = cu;        
        }
        // GET: ZonaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ZonaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       


        
    }
}
