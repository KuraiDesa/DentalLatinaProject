using Dental_Latina_MVC.Models;
using DTOs.DTOs;
using DTOs.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaAplicacion.InterfacesCasoUso;
using LogicaAplicacion.ServicioCorreo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Diagnostics;

namespace Dental_Latina_MVC.Controllers
{
    public class HomeController : Controller
    {


        public ILoginUser CULoginUser { get; set; }
        public IRegistroCliente CURegistroCliente { get; set; }
        public HomeController(ILoginUser CULoginUser, IRegistroCliente CURegistroCliente)
        {
            this.CULoginUser = CULoginUser;
            this.CURegistroCliente = CURegistroCliente;
        }

        public IActionResult Index()
        {
            return View();
        }
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public JsonResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return Json(new { success = false, error = "Respuesta invalida" });
            }

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Json(new { success = false, error = "Ingrese datos en ambos campos." });
            }

            LoguinUsuarioDTO loguinUsuarioDTO = new LoguinUsuarioDTO();
            loguinUsuarioDTO.mail = request.Email;
            loguinUsuarioDTO.contraseña = request.Password;

            if (CULoginUser.Login(loguinUsuarioDTO) != null)
            {
                HttpContext.Session.SetString("Usuario", loguinUsuarioDTO.mail);
                return Json(new { success = true, redirectUrl = "/Admin/Index" });
            }

            return Json(new { success = false, error2 = "Credenciales incorrectas." });
        }

        public class ingresoClienteRequest
        {
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string email { get; set; }
            public bool esEstudiante { get; set; }
        }
        [HttpPost]
        public JsonResult registroCliente([FromBody] ingresoClienteRequest request)
        {
            // Aquí hacés el registro en la base de datos
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult ingresoCliente([FromBody] ingresoClienteRequest request)
        {
            var servicioCorreo = new ServicioCorreo();
            string codigo = servicioCorreo.GenerarCodigo();
            string emailDestino = request.email;

            try
            {
                // Enviar correo
                //servicioCorreo.EnviarCodigoPorCorreo(emailDestino, codigo).Wait(); // Forzamos sincrónicamente para no hacer async todo
                return Json(new { success = true, codigo = codigo }); // 👈 ¡Devuelve el código!
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error al enviar el correo: " + ex.Message });
            }
        }



    }
}
