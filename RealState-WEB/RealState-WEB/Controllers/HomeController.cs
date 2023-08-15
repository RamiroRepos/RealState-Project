using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState_WEB.Model;
using RealState_WEB.Models;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace RealState_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Propiedades";
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var propiedadesJson = await respuesta.Content.ReadAsStringAsync();
                var propiedadesList = JsonSerializer.Deserialize<List<PROPIEDADES>>(propiedadesJson);

                // Filtrar las propiedades donde el estado es true
                var propiedadesFiltradas = propiedadesList.Where(p => p.estado == true).ToList();

                return View(propiedadesFiltradas);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(USUARIOS entidad)
        {
            try
            {
                using var client = new HttpClient();
                string email = entidad.email;
                string pass = entidad.contrasenna;
                var apiUrl = $"https://localhost:7273/api/Usuarios/ValidarUsuario?email={email}&pass={pass}";

                var respuesta = await client.GetAsync(apiUrl);

                if (respuesta.IsSuccessStatusCode)
                {
                    var UsuarioJson = await respuesta.Content.ReadAsStringAsync();
                    var Usuario = JsonSerializer.Deserialize<USUARIOS>(UsuarioJson);

                    HttpContext.Session.SetString("_nombre", Usuario.nombre);
                    HttpContext.Session.SetInt32("_id", (int)Usuario.id);
                    HttpContext.Session.SetInt32("_idRol", (int)Usuario.id_rol_fk);

                    return RedirectToAction("Index", "Home");
                }
                else if (respuesta.StatusCode == HttpStatusCode.NotFound)
                {
                    // El usuario no fue encontrado, puedes
                    // un mensaje de error o redirigir a una página de error
                    TempData["SweetAlertMessage"] = "Error al iniciar sesión";
                    TempData["SweetAlertType"] = "error";
                    return View();
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            try
            {
                HttpContext.Session.SetString("_nombre", "");
                HttpContext.Session.SetInt32("_id", 0);
                HttpContext.Session.SetInt32("_idRol", 0);
                return RedirectToAction("IniciarSesion");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            try
            {
                USUARIOS usuario = new USUARIOS();
                return View(usuario);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RecuperarContrasenna(USUARIOS entidad)
        {
            try
            {
                if (entidad.CodigoValidarUsuario == null && entidad.contrasenna == null)
                {
                    Random random = new Random();
                    entidad.CodigoValidarSistema = random.Next(100000, 999999);
                    string Asunto = "Recuperar Contraseña";
                    string Body = "Hola,<br/>"
                    + "Le hemos enviado este correo electrónico en respuesta a su solicitud de restablecer su contraseña.<br/>"
                    + "Por favor digite el siguiente código en el espacio que se solicita para continuar con el restablecimiento:<br/>"
                    + entidad.CodigoValidarSistema;
                    //EnviarCorreo(respuesta.email, Asunto, Body);
                }
                else if (entidad.CodigoValidarUsuario != null && entidad.CodigoValidarSistema != 0)
                {
                    if (entidad.CodigoValidarUsuario == entidad.CodigoValidarSistema)
                    {
                        entidad.CodigoValidado = true;
                        return View(entidad);
                    }
                    ViewBag.mensaje = "Código de verificación incorrecto";
                }
                else if (entidad.contrasenna != null)
                {
                    using var client = new HttpClient();
                    string email = entidad.email;
                    string pass = entidad.contrasenna;
                    var apiUrl = $"https://localhost:7273/api/Usuarios/RestablecerContrasenna?email={email}&pass={pass}";

                    var respuesta = await client.GetAsync(apiUrl);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var UsuarioJson = await respuesta.Content.ReadAsStringAsync();
                        var Usuario = JsonSerializer.Deserialize<USUARIOS>(UsuarioJson);
                        return RedirectToAction("IniciarSesion");
                    }
                }
                return View(entidad);
            }
            catch (Exception ex)
            {
                return View("Iniciar Sesion");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}