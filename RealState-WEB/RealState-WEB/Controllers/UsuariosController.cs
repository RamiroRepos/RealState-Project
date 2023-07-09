using Microsoft.AspNetCore.Mvc;
using RealState_WEB.Model;
using System.Text.Json;

namespace RealState_WEB.Controllers
{
    public class UsuariosController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ConsultarUsuarios()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Usuarios/Usuarios";
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var UsuariosJson = await respuesta.Content.ReadAsStringAsync();
                var UsuariosList = JsonSerializer.Deserialize<List<USUARIOS>>(UsuariosJson);
                return View(UsuariosList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarUsuario(long id)
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Usuarios/Usuario/" + id;
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var usuarioJson = await respuesta.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<USUARIOS>(usuarioJson);

                usuario.direccion.pais.paisesList = await Paises();
                usuario.direccion.provincia.provinciaList = await Provincias();
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
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
