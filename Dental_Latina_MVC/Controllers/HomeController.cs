using Dental_Latina_MVC.Models;
using DTOs.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasoUso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Dental_Latina_MVC.Controllers
{
    public class HomeController : Controller
    {


        public ILoginUser CULoginUser { get; set; }
        public HomeController(ILoginUser CULoginUser)
        {
            this.CULoginUser = CULoginUser;
        
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if(email != "" || password != "")
            {
                try
                {
                    LoguinUsuarioDTO loguinUsuarioDTO = new LoguinUsuarioDTO();
                    loguinUsuarioDTO.mail = email;
                    loguinUsuarioDTO.contraseña = password;
                   
                    if (CULoginUser.Login(loguinUsuarioDTO) != null)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Credenciales invalidas";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                   ViewBag.error = ex.Message;
                    return View();
                }

            }
            else
            {
                ViewBag.error = "Credenciales invalidas";
                return View();
            }
            

        }
    }
}
