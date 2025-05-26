using Dental_Latina_MVC.Models;
using DTOs.DTOs;
using DTOs.DTOs.UsuarioDTOs;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaAplicacion.InterfacesCasoUso;
using LogicaAplicacion.ServicioCorreo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Diagnostics;
using System.Security.Claims;

namespace Dental_Latina_MVC.Controllers
{
    public class HomeController : Controller
    {

        public IListarProductos CUListarProductos { get; set; }
        public ILoginUser CULoginUser { get; set; }
        public IRegistroCliente CURegistroCliente { get; set; }
        public HomeController(ILoginUser CULoginUser, IRegistroCliente CURegistroCliente, IListarProductos cUListarProductos)
        {
            this.CULoginUser = CULoginUser;
            this.CURegistroCliente = CURegistroCliente;
            this.CUListarProductos = cUListarProductos;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductoDTO> listaProductos = CUListarProductos.Get4Randoms();
            GeneralProductosHomeViewModel general = new GeneralProductosHomeViewModel
            {
                Productos = listaProductos
            };
            return View(general);
        }
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] LoginRequest request)
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

            var usuarioValido = CULoginUser.Login(loguinUsuarioDTO); // tu validación
            if (usuarioValido != null)
            {
                // Crear lista de Claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loguinUsuarioDTO.mail),
            new Claim(ClaimTypes.Role, "Admin") // solo si querés usar roles
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

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
            try
            {
                RegistroUsuarioDTO newUser = new RegistroUsuarioDTO();
                newUser.nombre = request.nombre;
                newUser.apellido = request.apellido;
                newUser.email = request.email;
                newUser.esEstudiante = request.esEstudiante;
                CURegistroCliente.RegistroClientes(newUser);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Algo salio mal" });
            }


        }

        [HttpPost]
        public JsonResult ingresoCliente([FromBody] ingresoClienteRequest request)
        {
            try
            {
                string emailDestino = request.email;
                if (CURegistroCliente.buscoMail(emailDestino))
                {
                    return Json(new { success = false, error = "Ya estas registrado!" });
                }


                var servicioCorreo = new ServicioCorreo();
                string codigo = servicioCorreo.GenerarCodigo();



                // Enviar correo
                servicioCorreo.EnviarCodigoPorCorreo(emailDestino, codigo).Wait();
                return Json(new { success = true, codigo = codigo });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error al enviar el correo: " + ex.Message });
            }
        }

        
    }
    public class GeneralProductosHomeViewModel
    {
        public IEnumerable<ProductoDTO> Productos { get; set; }

    }
}
