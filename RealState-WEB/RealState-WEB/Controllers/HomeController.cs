using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState_WEB.Filtros;
using RealState_WEB.Model;
using RealState_WEB.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
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
                if (entidad.CodigoValidarUsuario == 0 && entidad.contrasenna == null && entidad.CodigoValidado == false)
                {
                    Random random = new Random();
                    entidad.CodigoValidarSistema = random.Next(100000, 999999);
                    string Asunto = "Recuperación de Contraseña";
                    string Body = "<html><body style=\"font-family: Arial, sans-serif;\">"
                                + "<h2 style=\"color: #007BFF;\">Recuperación de Contraseña</h2>"
                                + "<p>Hola,</p>"
                                + "<p>Le hemos enviado este correo electrónico en respuesta a su solicitud de restablecer su contraseña.</p>"
                                + "<p>Por favor, ingrese el siguiente código en el espacio provisto para continuar con el restablecimiento:</p>"
                                + "<h3 style=\"background-color: #f0f0f0; padding: 10px; display: inline-block;\">"
                                + entidad.CodigoValidarSistema + "</h3>"
                                + "<p>Si no solicitó el restablecimiento de contraseña, puede ignorar este correo.</p>"
                                + "<p>¡Gracias!</p>"
                                + "</body></html>";
                    EnviarCorreo(entidad.email, Asunto, Body);
                }
                else if (entidad.CodigoValidarUsuario != 0 && entidad.CodigoValidarSistema != 0)
                {
                    if (entidad.CodigoValidarUsuario == entidad.CodigoValidarSistema)
                    {
                        entidad.CodigoValidado = true;
                        entidad.CodigoValidarUsuario = 0;
                        entidad.CodigoValidarSistema = 0;
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
                        string Asunto = "Recuperación de Contraseña Exitosa";
                        string LinkRecuperarContrasenna = "https://localhost:7277/Home/RecuperarContrasenna";
                        string BotonRecuperarContrasenna = "<a href='" + LinkRecuperarContrasenna + "' style='background-color: #007BFF; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>Cambiar Contraseña</a>";

                        string Body = "<html><body style=\"font-family: Arial, sans-serif;\">"
                                    + "<h2 style=\"color: #007BFF;\">Recuperación de Contraseña Exitosa</h2>"
                                    + "<p>Hola,</p>"
                                    + "<p>Se ha restablecido su contraseña correctamente.</p>"
                                    + "<p>Si no solicitó el restablecimiento de contraseña, le recomendamos cambiarla de inmediato.</p>"
                                    + "<p>" + BotonRecuperarContrasenna + "</p>"
                                    + "<p>¡Gracias por utilizar nuestro servicio!</p>"
                                    + "</body></html>";

                        EnviarCorreo(entidad.email, Asunto, Body);

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

        [HttpGet]
        public async Task<ActionResult> RegistrarUsuario()
        {
            try
            {
                USUARIOS usuario = new USUARIOS();
                usuario.direccion = new USUARIO_DIRECCIONES();
                usuario.direccion.pais = new PAISES();
                usuario.direccion.pais.paisesList = await Paises();
                usuario.direccion.provincia = new PROVINCIAS();
                usuario.direccion.provincia.provinciaList = await Provincias();
                return View(usuario);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(USUARIOS usuario)
        {
            try
            {
                usuario.id_rol_fk = 2;
                usuario.estado = true;
                if (usuario.direccion.pais.id != null)
                {
                    usuario.direccion.id_pais_fk = (long)usuario.direccion.pais.id;
                }
                usuario.direccion.pais.nombre = "";
                if (usuario.direccion.provincia.id != null)
                {
                    usuario.direccion.id_provincia_fk = (long)usuario.direccion.provincia.id;
                }
                usuario.direccion.provincia.nombre = "";
                usuario.rol = new USUARIO_ROLES();
                usuario.rol.id = 2;
                usuario.rol.nombre = "";
                usuario.rol.descripcion = "";

                if (ModelState.IsValid)
                {
                    if (usuario.confirmarContrasenna != null && usuario.confirmarContrasenna == usuario.contrasenna)
                    {
                        using var client = new HttpClient();
                        var apiUrl = "https://localhost:7273/api/Usuarios/NuevoUsuario";
                        JsonContent body = JsonContent.Create(usuario);
                        var response = await client.PostAsync(apiUrl, body);
                        if (response.IsSuccessStatusCode)
                        {
                            var propiedadJson = await response.Content.ReadAsStringAsync();
                            TempData["SweetAlertMessage"] = "Usuario registrado correctamente";
                            TempData["SweetAlertType"] = "success";
                            return RedirectToAction("IniciarSesion");
                        }
                        usuario.direccion.pais.paisesList = await Paises();
                        usuario.direccion.provincia.provinciaList = await Provincias();
                        TempData["SweetAlertMessage"] = "Error al registrar, favor contactar al administrador";
                        TempData["SweetAlertType"] = "error";
                        return View(usuario);
                    }
                    else
                    {
                        usuario.direccion.pais.paisesList = await Paises();
                        usuario.direccion.provincia.provinciaList = await Provincias();
                        TempData["SweetAlertMessage"] = "Las contraseñas no coinciden";
                        TempData["SweetAlertType"] = "error";
                        return View(usuario);
                    }
                }
                usuario.direccion.pais.paisesList = await Paises();
                usuario.direccion.provincia.provinciaList = await Provincias();
                return View(usuario);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public void EnviarCorreo(string Destinatario, string Asunto, string Body)
        {
            string EmailOrigen = "lcascante50329@ufide.ac.cr";
            string Contraseña = "8BEZ_RRAC";

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(Destinatario, "User"));
            msg.From = new MailAddress(EmailOrigen, "Admin");
            msg.Subject = Asunto;
            msg.Body = Body;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            client.Port = 587;
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Send(msg);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<List<PAISES>> Paises()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Paises/";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var paisesJson = await respuesta.Content.ReadAsStringAsync();
                var paisesList = JsonSerializer.Deserialize<List<PAISES>>(paisesJson);

                return paisesList;
            }
            else
            {
                return new List<PAISES>();
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<List<PROVINCIAS>> Provincias()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Provincias/";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var provinciasJson = await respuesta.Content.ReadAsStringAsync();
                var provinciasList = JsonSerializer.Deserialize<List<PROVINCIAS>>(provinciasJson);

                return provinciasList;
            }
            else
            {
                return new List<PROVINCIAS>();
            }
        }
    }
}