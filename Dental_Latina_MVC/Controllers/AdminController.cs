using Microsoft.AspNetCore.Mvc;

namespace Dental_Latina_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
