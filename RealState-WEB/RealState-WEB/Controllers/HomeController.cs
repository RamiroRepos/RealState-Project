using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState_WEB.Model;
using RealState_WEB.Models;
using System.Diagnostics;
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
                return View(propiedadesList);
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
                else
                {
                    return View();
                }
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}