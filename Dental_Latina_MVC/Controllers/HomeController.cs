using Dental_Latina_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dental_Latina_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
           
            throw new NotImplementedException();
            return View();
        }
    }
}
